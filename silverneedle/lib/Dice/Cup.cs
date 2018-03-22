// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Dice
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SilverNeedle.Serialization;
    
    public class Cup
    {
        public Cup()
        {
            this.Dice = new List<Die>();
        }

        public Cup(IList<Die> dice)
            : this()
        {
            this.Dice.AddRange(dice);
        }

        public Cup(IList<Die> dice, int modifier) : this(dice)
        {
            this.Modifier = modifier;
        }

        public Cup(Die die, int modifier) : this(die)
        {
            this.Modifier = modifier;
        }

        public Cup(Die die) : this()
        {
            this.AddDie(die);
        }

        public Cup(IObjectStore configuration) : this()
        {
             Name = configuration.GetString("name");
             this.ParseDice(configuration.GetString("dice"));
        }

        [ObjectStoreOptional("name")]
        public string Name { get; private set; }

        public List<Die> Dice { get; private set; }

        public int Modifier { get; set; }

        [ObjectStoreOptional("maximize-amount")]
        public bool MaximizeAmount { get; set; }

        public int Count { get { return Dice.Count; } }

        [ObjectStore("dice-value")]
        public string DiceString
        {
            get { return this.ToString(); }
            set 
            {
                this.ParseDice(value);
            }
        }

        public void AddDie(Die die)
        {
            this.Dice.Add(die);
        }

        public void AddDice(IEnumerable<Die> dice)
        {
            this.Dice.AddRange(dice);
        }

        public int Roll()
        {
            if(MaximizeAmount)
                return this.Dice.Sum(x => x.SideCount);
            int total = 0;
            foreach (Die d in this.Dice)
            {
                total += d.Roll();
            }

            return this.Modifier + total;
        }


        public int SumTop(int number)
        {
            return this.Dice
                .OrderByDescending(d =>
                {
                    return d.LastRoll;
                })
                .Take(number)
                .Sum(d =>
                {
                    return d.LastRoll;
                });
        }

        public override string ToString()
        {
            if(MaximizeAmount)
                return string.Format("{0} points", this.Roll());

            var diceGroups = this.Dice
                .GroupBy(die => die.Sides)
                .Select(group => new 
                { 
                    Sides = group.Key,
                    Count = group.Count()
                });
            var result = new StringBuilder();
            foreach (var d in diceGroups)
            {
                if (result.Length == 0)
                {
                    result.AppendFormat("{0}d{1}", d.Count, (int)d.Sides);
                }
                else
                {
                    result.AppendFormat("+{0}d{1}", d.Count, (int)d.Sides);
                }
            }

            if (this.Modifier > 0)
            {
                result.AppendFormat("+{0}", this.Modifier);
            }
            else if (this.Modifier < 0)
            {
                result.Append(this.Modifier);
            }

            return result.ToString();
        }

        public Cup Copy()
        {
            var copy = new Cup(this.Dice, Modifier);
            return copy;
        }
    }
}