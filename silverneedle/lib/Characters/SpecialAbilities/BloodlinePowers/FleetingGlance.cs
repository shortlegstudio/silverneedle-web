// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class FleetingGlance : SpecialAbility, IBloodlinePower, IComponent
    {
        private ClassLevel sorcererLevel;
        public int RoundsPerDay
        {
            get { return sorcererLevel.Level; }
        }
        public void Initialize(ComponentContainer components)
        {
            sorcererLevel = components.Get<ClassLevel>();
        }

        public override string Name
        {
            get
            {
                return "{0} rounds/day - {1}".Formatted(
                    RoundsPerDay,
                    base.Name
                );
            }
        }
    }
}