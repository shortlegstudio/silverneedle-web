//-----------------------------------------------------------------------
// <copyright file="NameGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.NamingThings
{
    using SilverNeedle;

    /// <summary>
    /// Name generator makes random names
    /// </summary>
    public class RandomSyllableNameGenerator : INameCharacter
    {
        /// <summary>
        /// Creates the full name.
        /// </summary>
        /// <returns>The full name.</returns>
        public string CreateFullName()
        {
            return string.Format("{0} {1}", this.CreateFirstName(), this.CreateLastName());
        }

        public string CreateFullName(Gender gender, string race)
        {
            return CreateFullName();
        }

        /// <summary>
        /// Builds the name from syllables.
        /// </summary>
        /// <returns>The name from syllables.</returns>
        /// <param name="syllables">Syllables to choose from.</param>
        /// <param name="count">Count of syllables.</param>
        public string BuildNameFromSyllables(string[] syllables, int count)
        {
            string name = string.Empty;

            for (var i = 0; i < count; i++)
            {
                name += syllables.ChooseOne();
            }

            return name.Capitalize();
        }

        /// <summary>
        /// Creates the first name.
        /// </summary>
        /// <returns>The first name.</returns>
        public string CreateFirstName()
        {
            return this.BuildNameFromSyllables(this.GetFirstNameSyllables(), Randomly.Range(1, 5));
        }

        /// <summary>
        /// Creates the last name.
        /// </summary>
        /// <returns>The last name.</returns>
        public string CreateLastName()
        {
            return this.BuildNameFromSyllables(this.GetLastNameSyllables(), Randomly.Range(1, 5));
        }

        /// <summary>
        /// Gets the first name syllables.
        /// </summary>
        /// <returns>The first name syllables.</returns>
        private string[] GetFirstNameSyllables()
        {
            return new string[]
            {
                "li",
                "pe",
                "le",
                "la",
                "hi",
                "wi",
                "ha",
                "hai",
                "'i",
                "na",
                "ne",
                "hei",
                "lei"
            };
        }

        /// <summary>
        /// Gets the last name syllables.
        /// </summary>
        /// <returns>The last name syllables.</returns>
        private string[] GetLastNameSyllables()
        {
            return new string[]
            {
                "li",
                "pe",
                "le",
                "la",
                "hi",
                "wi",
                "ha",
                "hai",
                "'i",
                "na",
                "ne",
                "hei",
                "lei"
            };
        }
    }
}