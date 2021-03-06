// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class ImprovedQuarry : AbilityDisplayAsName, IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            components.Remove<Quarry>();
        }
    }
}