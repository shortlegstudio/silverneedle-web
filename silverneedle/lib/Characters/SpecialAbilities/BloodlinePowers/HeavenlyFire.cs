// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Dice;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using SilverNeedle.Utility;
    public class HeavenlyFire : IAttackStatistic, IBloodlinePower, IComponent
    {
        private OffenseStats offense;
        private ClassLevel sorcererLevel;
        private AbilityScore charisma;
        public string Name { get { return "Heavenly Fire"; } }

        public IStatistic AttackBonus { get { return offense.RangeAttackBonus; } }

        public Cup Damage 
        {
            get
            {
                var cup = new Cup(Die.D4());
                cup.Modifier = sorcererLevel.Level / 2;
                return cup;
            }
        }

        public int NumberOfAttacks { get { return 1; } }

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public IStatistic CriticalModifier { get; private set; }

        public int CriticalThreat { get { return 20; } }

        public int SaveDC { get { return 0; } }

        public int Range { get { return 30; } }

        public int UsesPerDay { get { return 3 + charisma.TotalModifier; } }
        public string AttackBonusString()
        {
            return AttackBonus.TotalValue.ToModifierString();
        }

        public string DisplayString()
        {
            return "{0}/day - {1} {2} ({3} divine energy)".Formatted(
                UsesPerDay,
                Name,
                AttackBonusString(),
                Damage
            );
        }

        public void Initialize(ComponentContainer components)
        {
            this.offense = components.Get<OffenseStats>();
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            this.sorcererLevel = components.Get<ClassLevel>();
            this.CriticalModifier = new BasicStat("Heavenly Fire Critical Modifier", 2);
        }
    }
}