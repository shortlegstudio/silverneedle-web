// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class AuraOfDespair : IAbility, INameByType, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sourceLevel;

        public int RoundsPerDay
        {
            get
            {
                return sourceLevel.Level;
            }
        }

        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public string DisplayString()
        {
            return "{0} ({1} rounds/day)".Formatted(this.Name(), RoundsPerDay); 
        }
    }
}