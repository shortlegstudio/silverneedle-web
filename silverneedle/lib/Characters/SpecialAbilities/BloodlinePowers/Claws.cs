// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    public class Claws : IAttackStatistic, IBloodlinePower, IComponent
    {
        private AbilityScore charisma;
        private AbilityScore strength;
        private SizeStats size;
        private ClassLevel sorcererLevel;
        private const string BASE_DAMAGE = "1d4";
        private const string BASE_DAMAGE_LEVEL_SEVEN = "1d6";
        public string Name { get { return "Claws"; } }

        public IStatistic AttackBonus { get; private set; }


        public Cup Damage 
        {
            get
            {
                var cup = DiceStrings.ParseDice(
                    DamageTables.ConvertDamageBySize(GetBaseDamage(), size.Size)
                );
                cup.Modifier = strength.TotalModifier;
                return cup;
            }
        }

        private string GetBaseDamage()
        {
            if(this.sorcererLevel.Level >=7)
                return BASE_DAMAGE_LEVEL_SEVEN;
            return BASE_DAMAGE;
        }

        public int NumberOfAttacks { get { return 2; } }

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public IStatistic CriticalModifier { get; private set; }

        public int CriticalThreat { get { return 20; } }

        public int SaveDC { get { return 0; } }

        public int Range { get { return 0; } }
        public int RoundsPerDay { get { return 3 + charisma.TotalModifier; } }

        public string AttackBonusString()
        {
            return this.AttackBonus.TotalValue.ToModifierString();
        }

        public string DisplayString()
        {
            return "2 claws {0} ({1}{3}) {2} rounds/day{4}".Formatted(
                AttackBonusString(),
                Damage.ToString(),
                RoundsPerDay,
                BonusDamage(),
                BonusText()
            );
        }

        private string BonusDamage()
        {
            if(this.sorcererLevel.Level >= 11)
                return " plus 1d6 fire";
            return string.Empty;
        }

        private string BonusText()
        {
            if(this.sorcererLevel.Level >= 5)
                return " magical";

            return string.Empty;
        }

        public void Initialize(ComponentBag components)
        {
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            this.strength = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Strength);
            this.size = components.Get<SizeStats>();
            this.CriticalModifier = new BasicStat("Claws Critical Modifier", 2);
            
            var offense = components.Get<OffenseStats>();
            this.AttackBonus = new BasicStat("Claw Attack Bonus");
            this.AttackBonus.AddModifier(new StatisticStatModifier(this.AttackBonus.Name, offense.MeleeAttackBonus));
            this.sorcererLevel = components.Get<ClassLevel>();
        }
    }
}