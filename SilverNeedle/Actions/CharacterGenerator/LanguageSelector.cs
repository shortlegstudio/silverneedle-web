//-----------------------------------------------------------------------
// <copyright file="LanguageSelector.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Mechanics.CharacterGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;

    /// <summary>
    /// Language selector.
    /// </summary>
    public class LanguageSelector
    {
        /// <summary>
        /// The languages available
        /// </summary>
        private IEntityGateway<Language> languages;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Mechanics.CharacterGenerator.LanguageSelector"/> class.
        /// </summary>
        /// <param name="languages">Languages gateway to fetch languages from</param>
        public LanguageSelector(IEntityGateway<Language> languages)
        {
            this.languages = languages;
        }

        /// <summary>
        /// Picks the languages.
        /// </summary>
        /// <returns>The languages that were selected.</returns>
        /// <param name="race">Race to provide selection criteria.</param>
        /// <param name="bonusLanguages">Bonus languages available to select.</param>
        public IEnumerable<Language> PickLanguages(Race race, int bonusLanguages)
        {
            var result = new List<Language>();

            // Assign Known Languages
            foreach (var l in race.KnownLanguages)
            {
                result.Add(this.languages.All().First(x => x.Name == l));
            }

            for (var i = 0; i < bonusLanguages; i++)
            {
                var available = this.languages.All().Where(
                        x => !result.Any(r => r.Name == x.Name) && race.AvailableLanguages.Any(avail => x.Name == avail));

                if (available.Count() > 0)
                {
                    var language = available.ToList().ChooseOne();
                    result.Add(language);
                }
            }

            return result;
        }
    }
}