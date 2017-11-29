// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class GraveTouch : SpecialAbility, IBloodlinePower, IComponent
    {
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

        public override string Name
        {
            get
            {
                return "{0}/day - {1} {2}/rounds".Formatted(
                    UsesPerDay,
                    base.Name,
                    RoundsDuration
                );
            }
        }

        public void Initialize(ComponentBag components)
        {
            sorcererLevels = components.Get<ClassLevel>();
            charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }
    }
}