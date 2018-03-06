// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;

    public class DiceMaximizeAmountModifier : IDiceModifier
    {
        public void ProcessModifier(Cup dice)
        {
            dice.MaximizeAmount = true;
        }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; private set; }

        public DiceMaximizeAmountModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public DiceMaximizeAmountModifier(string name)
        {
            StatisticName = name;
        }
    }
}