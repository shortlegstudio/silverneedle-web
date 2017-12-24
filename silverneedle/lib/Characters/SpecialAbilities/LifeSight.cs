// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class LifeSight : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;
        public int RoundsPerDay
        {
            get { return sourceLevel.Level; }
        }

        public int Range
        {
            get
            {
                return 10 + ((sourceLevel.Level - 8) / 4) * 10;
            }
        }
        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public override string Name
        {
            get 
            {
                return "{0} {1}ft ({2} rounds/day)".Formatted(
                    base.Name,
                    Range,
                    RoundsPerDay
                );
            }
        }
    }
}