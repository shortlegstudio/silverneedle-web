// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class ChannelEnergy : SpecialAbility, IComponent
    {
        public ChannelEnergyAttack ChannelAttack { get; private set; }
        public void Initialize(ComponentBag components)
        {
            this.ChannelAttack = new ChannelEnergyAttack(components);
            components.Add(ChannelAttack);
        }

    }
}