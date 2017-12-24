// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class IncorporealForm : SpecialAbility, IBloodlinePower, IComponent
    {
        private ClassLevel  sorcererLevels;
        public int RoundsPerDay
        {
            get { return sorcererLevels.Level; }
        }

        public int UsesPerDay
        {
            get { return 1; }
        }

        public void Initialize(ComponentContainer components)
        {
            sorcererLevels = components.Get<ClassLevel>();
        }

        public override string Name
        {
            get
            {
                return "{0}/day {1} {2}/rounds".Formatted(
                    UsesPerDay,
                    base.Name,
                    RoundsPerDay
                );
            }
        }
    }
}