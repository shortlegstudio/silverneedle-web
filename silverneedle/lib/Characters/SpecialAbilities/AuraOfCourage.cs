// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;
    public class AuraOfCourage : IAbility, IComponent, INameByType
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var def = components.Get<DefenseStats>();
            def.AddImmunity("Fear");
        }

        public string DisplayString() 
        {
            return this.Name();
        }
    }
}