// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class DiceClassLevelModifier : IDiceModifier, IComponent
    {
        private ClassLevel classLevel;

        public DiceClassLevelModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public DiceClassLevelModifier(Cup dicePerLevel, string statName, string clsName, int rate)
        {
            this.DicePerLevel = dicePerLevel;
            this.StatisticName = statName;
            this.Class = clsName;
            this.Rate = rate;
        }

        public void Initialize(ComponentContainer components)
        {
            classLevel = components.Get<ClassLevel>();
        }

        [ObjectStore("dice")]
        public Cup DicePerLevel { get; private set; }

        public void ProcessModifier(Cup dice)
        {
            int diceCount = (classLevel.Level - StartLevel) / Rate;
            Repeat.Times(diceCount, () => dice.AddDice(DicePerLevel.Dice));
        }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStore("class")]
        public string Class { get; private set; }

        [ObjectStore("rate")]
        public int Rate { get; private set; }

        [ObjectStoreOptional("start-level")]
        public int StartLevel { get; private set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; private set; }

    }
}