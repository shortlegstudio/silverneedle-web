// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class GraveTouch : SpecialAbility, IBloodlinePower, IComponent
    {
        private AbilityScore baseAbility;
        private ClassLevel sourceLevels;
        private AbilityScoreTypes baseAbilityType;
        public int UsesPerDay
        {
            get 
            {
                return 3 + baseAbility.TotalModifier;
            }
        }

        public int RoundsDuration
        {
            get
            {
                return (sourceLevels.Level / 2).AtLeast(1);
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
            sourceLevels = components.Get<ClassLevel>();
            baseAbility = components.Get<AbilityScores>().GetAbility(baseAbilityType);
        }

        public GraveTouch(IObjectStore configuration)
        {
            baseAbilityType = configuration.GetEnum<AbilityScoreTypes>("base-ability");
        }
    }
}