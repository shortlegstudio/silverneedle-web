// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class CorruptingTouch : AbilityDisplayAsName, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private AbilityScore charisma;
        private ClassLevel sorcererLevels;
        public int UsesPerDay
        {
            get
            {
                return 3 + charisma.TotalModifier;
            }
        }

        public int RoundsDuration
        {
            get
            {
                return (sorcererLevels.Level / 2).AtLeast(1);
            }
        }
        public void Initialize(ComponentContainer components)
        {
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            this.sorcererLevels = components.Get<ClassLevel>();
        }
    }
}