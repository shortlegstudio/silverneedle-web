// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    public class CapabilityDiceStatistic : DiceStatistic, IAbility
    {
        public CapabilityDiceStatistic(IObjectStore configuration) : base(configuration)
        {
        }

        public override string DisplayString()
        {
            return "{0} ({1})".Formatted(this.Name, this.Dice.ToString());
        }
    }
}