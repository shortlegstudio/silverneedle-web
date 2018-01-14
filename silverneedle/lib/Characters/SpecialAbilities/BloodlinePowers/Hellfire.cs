// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class Hellfire : IAttackStatistic, IBloodlinePower, IComponent
    {
        private ClassLevel sorcererLevels;
        private AbilityScore charisma;
        public string Name { get { return "Hellfire"; } }

        public IValueStatistic AttackBonus => throw new System.NotImplementedException();

        public Cup Damage 
        {
            get
            {
                return new Cup(Die.GetDice(DiceSides.d6, sorcererLevels.Level));
            }
        }

        public int NumberOfAttacks => throw new System.NotImplementedException();

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public IValueStatistic CriticalModifier => throw new System.NotImplementedException();

        public int CriticalThreat => throw new System.NotImplementedException();

        public int SaveDC 
        {
            get
            {
                return 10 + charisma.TotalModifier + sorcererLevels.Level / 2;
            }
        }

        public int Range { get { return 10; } }
        public int UsesPerDay
        {
            get
            {
                if (sorcererLevels.Level >= 20)
                    return 3;
                if (sorcererLevels.Level >= 17)
                    return 2;
                return 1;
            }
        }

        public string AttackBonusString()
        {
            throw new System.NotImplementedException();
        }

        public string DisplayString()
        {
            return "{0}/day {1} {2} radius ({3} fire, DC {4})".Formatted(
                UsesPerDay,
                this.Name,
                Range.ToRangeString(),
                Damage,
                SaveDC
            );
        }

        public void Initialize(ComponentContainer components)
        {
            this.sorcererLevels = components.Get<ClassLevel>();
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }
    }
}