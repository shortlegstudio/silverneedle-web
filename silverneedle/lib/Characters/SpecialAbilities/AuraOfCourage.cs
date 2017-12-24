// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;
    public class AuraOfCourage : SpecialAbility, IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            var def = components.Get<DefenseStats>();
            def.AddImmunity("Fear");
        }
    }
}