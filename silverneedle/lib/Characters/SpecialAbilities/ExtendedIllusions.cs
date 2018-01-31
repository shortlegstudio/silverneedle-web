// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class ExtendedIllusions : IAbility, INameByType, IComponent
    {
        private ClassLevel sourceLevel;
        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public int Duration
        {
            get { return (sourceLevel.Level / 2).AtLeast(1); }
        }

        public string DisplayString()
        {
            return "{0} ({1} rounds)".Formatted(this.Name(), Duration);
        }
    }
}