// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class FlurryOfBlows : IAttackStatistic, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private DataTable monkAbilities;
        private ClassLevel monkLevels;
        private UnarmedMonk unarmedStrike;
        private CharacterSize size;

        public FlurryOfBlows() 
            : this(GatewayProvider.Find<DataTable>("Monk Abilities"))
        {
        }

        public FlurryOfBlows(DataTable monkAbilities)
        {
            this.monkAbilities = monkAbilities;
            this.Name = "Flurry of Blows";
            this.AttackBonus = new BasicStat("Flurry of Blows Attack Bonus", 0);
            this.DamageModifier = new BasicStat("Flurry of Blows Damage Modifier", 0);
            this.CriticalModifier = new BasicStat("Flurry of Blows Critical Modifier", 2);
        }

        public void Initialize(ComponentContainer components)
        {
            this.monkLevels = components.Get<ClassLevel>();
            for(int i = 1; i < 10; i++)
            {
                this.AttackBonus.AddModifier(
                    new ConditionalStatModifier(
                        new FlurryOfBlowsAttackBonusModifier(this, i),
                        "attack {0}".Formatted(i)
                    )
                );
            }
            var strength = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Strength);
            this.AttackBonus.AddModifier(strength.UniversalStatModifier);
            this.DamageModifier.AddModifier(strength.UniversalStatModifier);
            var monk = components.Get<MonkUnarmedStrike>();
            this.unarmedStrike = monk.Weapon;
            this.size = components.Get<SizeStats>().Size;
        }

        public int NumberOfAttacks
        {
            get
            {
                return GetFlurryOfBlowsAttackBonuses().Count();
            }
        }

        public string Name { get; private set; }

        public IValueStatistic AttackBonus { get; private set; }

        public IValueStatistic DamageModifier { get; private set; }

        public virtual Cup Damage 
        { 
            get 
            { 
                var damageDice = DiceStrings.ParseDice(
                    DamageTables.ConvertDamageBySize(this.unarmedStrike.Damage, this.size)
                );
                damageDice.Modifier += DamageModifier.TotalValue;
                return damageDice;
            }
        }

        public AttackTypes AttackType { get { return AttackTypes.Melee; } }

        public IValueStatistic CriticalModifier { get; private set; }

        public int CriticalThreat { get; private set; }
        public int SaveDC { get { return 0; } }
        public int Range { get { return 0; } }

        public string DisplayString()
        {
            if(unarmedStrike == null)
                return "Flurry of Blows - not initialized";
            
            return string.Format(
                "{0} {1} ({2} / {3}x{4})",
                this.Name,
                this.AttackBonusString(),
                this.Damage,
                this.CriticalThreat,
                this.CriticalModifier.TotalValue);
        }

        private IEnumerable<int> GetFlurryOfBlowsAttackBonuses()
        {
            if(monkLevels == null)
                return new int[] { 0 };
            var row = monkAbilities.Get(monkLevels.Level.ToString(), "flurry-of-blows");
            var bonuses = row.Split('/');
            return bonuses.Select(x => x.ToInteger());
        }

        private class FlurryOfBlowsAttackBonusModifier : DelegateStatModifier
        {
            public FlurryOfBlowsAttackBonusModifier(FlurryOfBlows blows, int attackNumber) 
                : base("Attack Bonus", "Class Ability", 
                 () => {return blows.GetFlurryOfBlowsAttackBonuses().ElementAt(attackNumber - 1);})
            {

            }
        }
        public string AttackBonusString()
        {
            List<string> bonuses = new List<string>();
            for(int i = 1; i <= NumberOfAttacks; i++)
            {
                bonuses.Add(
                    this.AttackBonus.GetConditionalValue("attack {0}".Formatted(i)).ToModifierString()
                );
            }
            return string.Join("/", bonuses);
        }
    }
}