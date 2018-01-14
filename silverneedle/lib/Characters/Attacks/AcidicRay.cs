// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using SilverNeedle.Utility;
    public class AcidicRay : IAttackStatistic, IBloodlinePower, IComponent
    {
        private OffenseStats offense;
        private ClassLevel sorcererLevels;
        private AbilityScore charisma;
        public string Name { get { return "Acidic Ray"; } }

        public IValueStatistic AttackBonus { get { return this.offense.RangeAttackBonus; } }

        public IValueStatistic DamageModifier { get; private set; }

        public Cup Damage 
        { 
            get
            {
                var dmg = new Cup(Die.D6());
                dmg.Modifier = DamageModifier.TotalValue;
                return dmg;
            }
        }

        public int NumberOfAttacks { get { return 1; } }

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public IValueStatistic CriticalModifier { get; private set; }

        public int CriticalThreat { get { return 20; } }

        public int SaveDC { get { return 0; } }

        public int Range { get { return 30; } }

        public string AttackBonusString()
        {
            return AttackBonus.TotalValue.ToModifierString();
        }

        public int UsesPerDay { get { return 3 + charisma.TotalModifier; } }

        public AcidicRay()
        {
            this.DamageModifier = new BasicStat("Acidic Ray Damage Modifier");
            this.CriticalModifier = new BasicStat("Acidic Ray Critical Modifier", 2);
        }

        public void Initialize(ComponentContainer components)
        {
            this.offense = components.Get<OffenseStats>();
            this.sorcererLevels = components.Get<ClassLevel>();
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            this.DamageModifier.AddModifier(new DelegateStatModifier(this.DamageModifier.Name, "bonus", this.Name, () => { return this.sorcererLevels.Level / 2; }));
        }

        public string DisplayString()
        {
            
            return string.Format(
                "{0}/day {1} {2} ({3} acid) {4}",
                this.UsesPerDay,
                this.Name,
                this.AttackBonusString(),
                this.Damage,
                this.Range.ToRangeString());
        }
    }
}