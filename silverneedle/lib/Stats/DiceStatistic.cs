// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using System.Collections.Generic;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;

    public class DiceStatistic : IDiceStatistic
    {
        private IList<IDiceModifier> modifiers = new List<IDiceModifier>();
        private IList<IValueStatModifier> valueModifiers = new List<IValueStatModifier>();
        public DiceStatistic(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public DiceStatistic(string name, string dice)
        {
            this.Name = name;
            this.BaseDice = DiceStrings.ParseDice(dice);
        }

        [ObjectStore("name")]
        public string Name { get; private set; }

        [ObjectStore("dice")]
        public Cup BaseDice { get; private set; }

        public Cup Dice 
        {
            get
            {
                var dice = BaseDice.Copy();
                foreach(var mod in modifiers)
                    mod.ProcessModifier(dice);

                foreach(var vm in valueModifiers)
                    dice.Modifier += (int)vm.Modifier;

                return dice;
            }
        }

        public void AddModifier(IStatisticModifier modifier)
        {
            if(modifier is IDiceModifier)
                modifiers.Add((IDiceModifier) modifier);
            else if(modifier is IValueStatModifier)
                valueModifiers.Add((IValueStatModifier)modifier);

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