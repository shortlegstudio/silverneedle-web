// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

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