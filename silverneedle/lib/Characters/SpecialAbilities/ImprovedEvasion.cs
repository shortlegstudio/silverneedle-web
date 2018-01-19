// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class ImprovedEvasion : IAbility, IComponent, INameByType
    {
        public void Initialize(ComponentContainer components)
        {
            components.Remove<Evasion>();
        }

        public string DisplayString()
        {
            return this.Name();
        }

    }
}