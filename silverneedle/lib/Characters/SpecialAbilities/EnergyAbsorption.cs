// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class EnergyAbsorption : IAbility, INameByType, IComponent
    {
        private ClassLevel sourceLevel;
        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public int Amount
        {
            get { return 3 * sourceLevel.Level; }
        }

        public string DisplayString()
        {
            return "{0} ({1} pts/day)".Formatted(this.Name(), Amount); 
        }
    }
}