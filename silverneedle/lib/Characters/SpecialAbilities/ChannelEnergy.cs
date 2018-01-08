// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class ChannelEnergy : IAbility, IComponent
    {
        public ChannelEnergyAttack ChannelAttack { get; private set; }

        public string DisplayString()
        {
            return "Channel Energy";
        }

        public void Initialize(ComponentContainer components)
        {
            this.ChannelAttack = new ChannelEnergyAttack(components);
            components.Add(ChannelAttack);
        }

    }
}