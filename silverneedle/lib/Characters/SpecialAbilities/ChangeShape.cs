// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class ChangeShape : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;
        public void Initialize(ComponentBag components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public int RoundsPerDay
        {
            get { return sourceLevel.Level; }
        }

        public override string Name
        {
            get { return "{0} ({1} rounds/day)".Formatted(base.Name, RoundsPerDay); }
        }
    }
}