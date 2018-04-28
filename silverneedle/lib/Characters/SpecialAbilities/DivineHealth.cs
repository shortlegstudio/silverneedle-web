// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Utility;

    public class DivineHealth : AbilityDisplayAsName, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var def = components.Get<DefenseStats>();
            def.AddImmunity("Disease");
        }
    }
}