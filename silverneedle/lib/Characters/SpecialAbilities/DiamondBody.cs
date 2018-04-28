// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using SilverNeedle.Utility;

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class DiamondBody : AbilityDisplayAsName, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            components.Get<DefenseStats>().AddImmunity("poison");
        }
    }
}