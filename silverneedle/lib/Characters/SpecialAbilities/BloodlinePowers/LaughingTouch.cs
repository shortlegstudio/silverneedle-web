// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class LaughingTouch : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private AbilityScore charisma;

        public int UsesPerDay
        {
            get
            {
                return 3 + charisma.TotalModifier;
            }
        }
        public void Initialize(ComponentContainer components)
        {
            charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }

        public string DisplayString()
        {
            return "{0}/day {1}".Formatted(
                UsesPerDay,
                this.Name()
            );
        }
    }
}