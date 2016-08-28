//-----------------------------------------------------------------------
// <copyright file="Cup.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Dice
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// A cup represents a dice cup that can hold multiple dice for rolling. 
    /// Modifiers can also be placed with the roll.
    /// </summary>
    public class Cup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Dice.Cup"/> class.
        /// </summary>
        public Cup()
        {
            this.Dice = new List<Die>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Dice.Cup"/> class.
        /// </summary>
        /// <param name="dice">Dice to initialize the cup with</param>
        public Cup(IList<Die> dice)
            : this()
        {
            this.Dice.AddRange(dice);
        }

        /// <summary>
        /// Gets the dice that are within the cup
        /// </summary>
        public List<Die> Dice { get; private set; }

        /// <summary>
        /// Gets or sets the modifier.
        /// </summary>
        /// <value>The modifier to adjust the dice roll</value>
        public int Modifier { get; set; }

        /// <summary>
        /// Adds a die to the cup
        /// </summary>
        /// <param name="die">Die to add to cup</param>
        public void AddDie(Die die)
        {
            this.Dice.Add(die);
        }

        /// <summary>
        /// Adds a list of dice to the cup
        /// </summary>
        /// <param name="dice">Dice to add to cup</param>
        public void AddDice(IEnumerable<Die> dice)
        {
            this.Dice.AddRange(dice);
        }

        /// <summary>
        /// Rolls the dice in the cup and returns the result. Applies any modifiers set
        /// </summary>
        /// <returns>The sum of the roll of the dice</returns>
        public int Roll()
        {
            int total = 0;
            foreach (Die d in this.Dice)
            {
                total += d.Roll();
            }

            return this.Modifier + total;
        }

        /// <summary>
        /// Rolls the dice and selects the top number of dice. Does not apply modifiers
        /// </summary>
        /// <returns>The sum of the top number of dice</returns>
        /// <param name="number">How many dice to select</param>
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

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Dice.Cup"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Dice.Cup"/>.</returns>
        public override string ToString()
        {
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
    }
}