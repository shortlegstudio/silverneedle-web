// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class BlindingRay : IAbility, INameByType, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private AbilityScore baseAbility;

        public int UsesPerDay
        {
            get { return 3 + baseAbility.TotalModifier; }
        }

        public void Initialize(ComponentContainer components)
        {
            baseAbility = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
        }

        public string DisplayString()
        {
            return "{0} ({1}/day)".Formatted(this.Name(), UsesPerDay);
        }

    }
}