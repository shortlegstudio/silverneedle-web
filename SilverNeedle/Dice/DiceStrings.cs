//-----------------------------------------------------------------------
// <copyright file="DiceStrings.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Dice
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A static class for handling dice strings like "2d6+4"
    /// </summary>
    public static class DiceStrings
    {
        /// <summary>
        /// Parses the sides of the dice into an enumeration version
        /// </summary>
        /// <returns>The sides to parse</returns>
        /// <param name="die">The die represented by the string</param>
        public static DiceSides ParseSides(string die)
        {
            return (DiceSides)Enum.Parse(typeof(DiceSides), die);
        }

        /// <summary>
        /// Parses the dice string and returns a rollable cup for the string representation.
        /// Dice string can be a basic dice roll representation for example "2d8+4" or "3d6-1"
        /// </summary>
        /// <returns>A rollable cup for this dice string</returns>
        /// <param name="diceString">Dice string to parse</param>
        public static Cup ParseDice(string diceString)
        {
            var cup = new Cup();

            var regEx = new Regex("^(?<dieCount>\\d+)?d(?<dieSides>\\d+)(?<modifier>\\+\\d+)?");
            var match = regEx.Match(diceString);
            var dieCount = DefaultOrNumber(match.Groups["dieCount"].Value, 1);
            var dieSides = DefaultOrNumber(match.Groups["dieSides"].Value, 1);
            var modifier = DefaultOrNumber(match.Groups["modifier"].Value, 0);

            var dice = Die.GetDice((DiceSides)dieSides, dieCount);
            cup.AddDice(dice);
            cup.Modifier = modifier;

            return cup;
        }

        /// <summary>
        /// Simple helper that returns the specified default or the value for a string.
        /// Helps ensure 1 is the default if no value is present in the string
        /// </summary>
        /// <returns>The best value.</returns>
        /// <param name="val">Value to parse</param>
        /// <param name="def">Default to replace if value is empty</param>
        private static int DefaultOrNumber(string val, int def)
        {
            if (string.IsNullOrEmpty(val))
            {
                return def;
            }
            else
            {
                return int.Parse(val);
            }
        }
    }
}
