// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class HolyChampion : IAbility, IComponent, INameByType
    {
        public void Initialize(ComponentContainer components)
        {
            var def = components.Get<DefenseStats>();
            def.AddDamageResistance(new EnergyResistance(5, "evil"));
            var lay = components.Get<LayOnHands>();
            lay.MaximizeAmount = true;

            var channel = components.Get<ChannelEnergy>();
            var maxChannelDice = new DiceMaximizeAmountModifier(channel.DamageDice.Name);
            components.Add(maxChannelDice);
        }

        public string DisplayString()
        {
            return this.Name();
        }

    }
}