// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;

    /// <summary>
    /// Damage tables.
    /// </summary>
    public static class DamageTables
    {
        /// <summary>
        /// The tiny damage table.
        /// </summary>
        private static List<string> tinyDamageTable = new List<string> 
        { 
            "0",    "1",    "1d2",  "1d3",  "1d4",  "1d6",  "1d8",  "1d4",  "1d8",  "1d10", "2d6"   
        };

        /// <summary>
        /// The small damage table.
        /// </summary>
        private static List<string> smallDamageTable = new List<string>
        { 
            "1",  "1d2",  "1d3",  "1d4",  "1d6",  "1d8",  "1d10", "1d6",  "1d10", "2d6",  "2d8"
        };

        /// <summary>
        /// The medium damage table.
        /// </summary>
        private static List<string> mediumDamageTable = new List<string>
        { 
            "1d2",  "1d3",  "1d4",  "1d6",  "1d8",  "1d10", "1d12", "2d4",  "2d6",  "2d8",  "2d10" 
        };

        /// <summary>
        /// The large damage table.
        /// </summary>
        private static List<string> largeDamageTable = new List<string> 
        { 
            "1d3",  "1d4",  "1d6",  "1d8",  "2d6",  "2d8",  "3d6",  "2d6",  "3d6",  "3d8",  "4d8" 
        };

        /// <summary>
        /// Converts the size of the damage by.
        /// </summary>
        /// <returns>The damage by size.</returns>
        /// <param name="mediumDamageAmount">Medium damage amount.</param>
        /// <param name="size">Size of the character.</param>
        public static string ConvertDamageBySize(string mediumDamageAmount, CharacterSize size)
        {
            //Drop the modifier
            Cup dice = DiceStrings.ParseDice(mediumDamageAmount);
            return ConvertDamageBySize(dice, size).ToString();
        }

        public static Cup ConvertDamageBySize(Cup dice, CharacterSize size)
        {
            Cup converted = new Cup();
            converted.Modifier = dice.Modifier;
            dice.Modifier = 0;
            var dieString = dice.ToString();

            int index = mediumDamageTable.IndexOf(dieString);
            if(index == -1)
                throw new DamageTableValueNotFoundException(dieString);

            switch (size)
            {
                case CharacterSize.Tiny:
                    converted.AddDice(DiceStrings.ParseDice(tinyDamageTable[index]).Dice);
                    break;
                case CharacterSize.Small:
                    converted.AddDice(DiceStrings.ParseDice(smallDamageTable[index]).Dice);
                    break;
                case CharacterSize.Medium:
                    converted.AddDice(DiceStrings.ParseDice(mediumDamageTable[index]).Dice);
                    break;
                case CharacterSize.Large:
                    converted.AddDice(DiceStrings.ParseDice(largeDamageTable[index]).Dice);
                    break;
                default:
                    throw new NotImplementedException(string.Format("Character Size: {0} has not been implemented in damage tables.", size));
            }

            return converted;
        }
    }
}