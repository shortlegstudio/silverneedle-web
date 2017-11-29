// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class LaughingTouch : SpecialAbility, IBloodlinePower, IComponent
    {
        private AbilityScore charisma;

        public int UsesPerDay
        {
            get
            {
                return 3 + charisma.TotalModifier;
            }
        }
        public void Initialize(ComponentBag components)
        {
            charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }

        public override string Name
        {
            get
            {
                return "{0}/day {1}".Formatted(
                    UsesPerDay,
                    base.Name
                );
            }
        }
    }
}