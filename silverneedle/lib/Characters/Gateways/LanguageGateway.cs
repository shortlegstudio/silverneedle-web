//-----------------------------------------------------------------------
// <copyright file="LanguageYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    /// <summary>
    /// Language yaml gateway.
    /// </summary>
    public class LanguageGateway : IEntityGateway<Language>
    {
        /// <summary>
        /// The language data file.
        /// </summary>
        private const string LanguageDataFileType = "language";

        /// <summary>
        /// The languages that are loaded.
        /// </summary>
        private IList<Language> languages = new List<Language>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.LanguageYamlGateway"/> class.
        /// </summary>
        public LanguageGateway()
        {
            var dataFiles = DatafileLoader.Instance.GetDataFiles(LanguageDataFileType);
            foreach(var y in dataFiles) {
                this.LoadObjects(y);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.LanguageYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml node to parse for languages</param>
        public LanguageGateway(IObjectStore dataStore)
        {
            this.LoadObjects(dataStore);
        }

        /// <summary>
        /// Should return all of this entity types
        /// </summary>
        /// <returns>Enumerable collection of the entities</returns>
        public IEnumerable<Language> All()
        {
            return this.languages;
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <param name="dataStore">Yaml to parse.</param>
        private void LoadObjects(IObjectStore dataStore)
        {
            foreach (var n in dataStore.Children)
            {
                this.languages.Add(new Language(
                        n.GetString("name"),
                        n.GetString("description")));
            }
        }
    }
}