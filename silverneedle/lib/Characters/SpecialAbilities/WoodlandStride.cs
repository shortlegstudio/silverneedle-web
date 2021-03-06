// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    public class WoodlandStride : IAbility, IBloodlinePower, INameByType
    {
        public string DisplayString()
        {
            return this.Name();
        }
    }
}