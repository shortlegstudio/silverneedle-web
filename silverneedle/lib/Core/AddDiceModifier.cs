// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using SilverNeedle.Serialization;

    public class AddDiceModifier : IDiceModifier
    {

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; private set; }

        [ObjectStore("dice")]
        public Dice.Cup Dice { get; private set; }

        public void ProcessModifier(Dice.Cup dice)
        {
            dice.AddDice(Dice.Dice);
        }

        public AddDiceModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }
    }
}