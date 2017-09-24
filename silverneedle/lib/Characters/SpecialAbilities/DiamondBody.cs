// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using SilverNeedle.Utility;

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class DiamondBody : SpecialAbility, IComponent
    {
        public void Initialize(ComponentBag components)
        {
            components.Get<DefenseStats>().AddImmunity("poison");
        }
    }
}