//-----------------------------------------------------------------------
// <copyright file="CharacterStringFormatting.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    /// <summary>
    /// Character string formatting provides functionality to make nicer strings for character 
    /// sheet display
    /// </summary>
    public static class CharacterStringFormatting
    {
        /// <summary>
        /// Shortens the CharacterAlignment string into standard 2 character format
        /// </summary>
        /// <returns>The shortened alignment string.</returns>
        /// <param name="alignment">Alignment to shorten.</param>
        public static string ShortString(this CharacterAlignment alignment)
        {
            switch (alignment)
            {
                case CharacterAlignment.LawfulGood:
                    return "LG";
                case CharacterAlignment.NeutralGood:
                    return "NG";
                case CharacterAlignment.ChaoticGood:
                    return "CG";
                case CharacterAlignment.LawfulNeutral:
                    return "LN";
                case CharacterAlignment.Neutral:
                    return "N";
                case CharacterAlignment.ChaoticNeutral:
                    return "CN";
                case CharacterAlignment.LawfulEvil:
                    return "LE";
                case CharacterAlignment.NeutralEvil:
                    return "NE";
                case CharacterAlignment.ChaoticEvil:
                    return "CE";
            }

            return "??";
        }

        /// <summary>
        /// Converts a number into a modifier string. This basically appends a +/- in front 
        /// of the string
        /// </summary>
        /// <returns>The number in modifier string format.</returns>
        /// <param name="value">Value to convert.</param>
        public static string ToModifierString(this int value)
        {
            if (value >= 0)
            {
                return string.Format("+{0}", value);
            }

            return value.ToString();
        }

   }
}