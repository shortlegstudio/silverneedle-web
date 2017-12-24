// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class HolyChampion : SpecialAbility, IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            var def = components.Get<DefenseStats>();
            def.AddDamageResistance(new DamageResistance(5, "evil"));
            var lay = components.Get<LayOnHands>();
            lay.MaximizeAmount = true;

            var channel = components.Get<ChannelEnergy>();
            channel.ChannelAttack.MaximizeAmount = true;
        }

    }
}