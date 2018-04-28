// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class FleetingGlance : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sorcererLevel;
        public int RoundsPerDay
        {
            get { return sorcererLevel.Level; }
        }
        public void Initialize(ComponentContainer components)
        {
            sorcererLevel = components.Get<ClassLevel>();
        }

        public string DisplayString()
        {
            return "{0} rounds/day - {1}".Formatted(
                RoundsPerDay,
                this.Name()
            );
        }
    }
}