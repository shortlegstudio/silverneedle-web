// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class HandOfTheApprentice : SpecialAbility, IComponent
    {
        private AbilityScore baseAbility;
        public void Initialize(ComponentBag components)
        {
            baseAbility = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
        }

        public int UsesPerDay
        {
            get { return 3 + baseAbility.TotalModifier; }
        }

        public override string Name
        {
            get { return "{0} ({1}/day)".Formatted(base.Name, UsesPerDay); }
        }
    }
}