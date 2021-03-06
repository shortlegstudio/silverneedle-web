// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class UncannyDodge : INameByType, IAbility
    {
        public string DisplayString()
        {
            return this.Name();
        }
    }
}