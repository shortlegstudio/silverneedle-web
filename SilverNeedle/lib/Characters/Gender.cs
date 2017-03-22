//-----------------------------------------------------------------------
// <copyright file="Gender.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    /// <summary>
    /// Gender of the character
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// The male.
        /// </summary>
        Male,

        /// <summary>
        /// The female.
        /// </summary>
        Female
    }

    public static class GenderExtensions
    {
        public static string Pronoun(this Gender gender)
        {
            return gender == Gender.Female ? "she" : "he";
        }

        public static string PossessivePronoun(this Gender gender)
        {
            return gender == Gender.Female ? "her" : "his";
        }
    }
}