//-----------------------------------------------------------------------
// <copyright file="DamageTables.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Equipment
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters;

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
            "1",    "1d2",  "1d3",  "1d4",  "1d6",  "1d8",  "1d10", "1d6",  "1d10", "2d6",  "2d8"
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
            int index = mediumDamageTable.IndexOf(mediumDamageAmount);
            switch (size)
            {
                case CharacterSize.Tiny:
                    return tinyDamageTable[index];
                case CharacterSize.Small:
                    return smallDamageTable[index];
                case CharacterSize.Medium:
                    return mediumDamageAmount;
                case CharacterSize.Large:
                    return largeDamageTable[index];
            }

            throw new NotImplementedException(string.Format("Character Size: {0} has not been implemented in damage tables.", size));
        }
    }
}