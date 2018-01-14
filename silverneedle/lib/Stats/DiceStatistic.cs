// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;

    public class DiceStatistic : IDiceStatistic
    {
        public DiceStatistic(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public DiceStatistic(string name, string dice)
        {
            this.Name = name;
            this.Dice = DiceStrings.ParseDice(dice);
        }

        [ObjectStore("name")]
        public string Name { get; private set; }

        [ObjectStore("dice")]
        public Cup Dice { get; private set; }

        public void AddModifier(IStatisticModifier modifier)
        {
            HandleModifier((IDiceModifier)modifier);
        }
        private void HandleModifier(IDiceModifier modifier)
        {
            this.Dice.AddDice(modifier.Dice.Dice);
        }

        public string DisplayString()
        {
            return Dice.ToString();
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}