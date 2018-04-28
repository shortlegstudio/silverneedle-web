// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class IncorporealForm : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
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

        public string DisplayString()
        {
            return "{0}/day {1} {2}/rounds".Formatted(
                UsesPerDay,
                this.Name(),
                RoundsPerDay
            );
        }
    }
}