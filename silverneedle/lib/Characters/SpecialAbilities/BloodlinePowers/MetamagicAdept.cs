// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class MetamagicAdept : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sorcererLevel;

        public void Initialize(ComponentContainer components)
        {
            sorcererLevel = components.Get<ClassLevel>();
        }

        public int UsesPerDay
        {
            get { return 1 + (sorcererLevel.Level - 3) / 4; }
        }

        public string DisplayString()
        {
            return "{0} {1}/day".Formatted(this.Name(), UsesPerDay);
        }
    }
}