//-----------------------------------------------------------------------
// <copyright file="LanguageSelector.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Language selector.
    /// </summary>
    public class LanguageSelector : ICharacterDesignStep
    {
        /// <summary>
        /// The languages available
        /// </summary>
        private EntityGateway<Language> languages;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGeneration.LanguageSelector"/> class.
        /// </summary>
        /// <param name="languages">Languages gateway to fetch languages from</param>
        public LanguageSelector(EntityGateway<Language> languages)
        {
            this.languages = languages;
        }

        public LanguageSelector()
        {
            this.languages = GatewayProvider.Get<Language>();
        }

        public void ExecuteStep(CharacterSheet character)
        {
            var strategy = character.Strategy;
            var known = this.languages.FindAll(strategy.LanguagesKnown);
            foreach(var k in known)
            {
                character.Add(k);
            }

            var bonusLanguages = character.AbilityScores.GetModifier(AbilityScoreTypes.Intelligence);

            IEnumerable<Language> available;
            if(strategy.LanguageChoices.Contains("ALL"))
            {
                available = this.languages.All();
            } else {
                available = strategy.LanguageChoices.Select(
                    option => this.languages.Find(option)
                );
            }

            for (var i = 0; i < bonusLanguages; i++)
            {
                //Keep trimming down options
                available = available.Where(x => !character.GetAll<Language>().Contains(x));

                if (available.Count() > 0)
                {
                    var language = available.ChooseOne();
                    character.Add(language);
                }
            }
        }
    }
}