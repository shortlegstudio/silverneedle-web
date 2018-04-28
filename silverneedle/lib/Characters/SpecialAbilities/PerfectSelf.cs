// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class PerfectSelf : AbilityDisplayAsName, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var def = components.Get<DefenseStats>();
            def.AddDamageResistance(new EnergyResistance(10, "chaotic"));
        }
    }
}