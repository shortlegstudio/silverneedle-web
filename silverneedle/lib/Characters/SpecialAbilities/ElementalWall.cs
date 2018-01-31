// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class ElementalWall : IAbility, INameByType, IComponent
    {
        private ClassLevel sourceLevel;
        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public int RoundsPerDay
        {
            get { return sourceLevel.Level; }
        }

        public string DisplayString()
        {
            return "{0} ({1} rounds/day)".Formatted(this.Name(), RoundsPerDay);
        }
    }
}