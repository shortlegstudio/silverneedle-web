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
    using SilverNeedle.Yaml;

    /// <summary>
    /// Language yaml gateway.
    /// </summary>
    public class LanguageYamlGateway : IEntityGateway<Language>
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
        public LanguageYamlGateway()
        {
            var yamlFiles = DatafileLoader.Instance.GetYamlFiles(LanguageDataFileType);
            foreach(var y in yamlFiles) {
                this.LoadFromYaml(y);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.LanguageYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml node to parse for languages</param>
        public LanguageYamlGateway(YamlNodeWrapper yaml)
        {
            this.LoadFromYaml(yaml);
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
        /// <param name="yaml">Yaml to parse.</param>
        private void LoadFromYaml(YamlNodeWrapper yaml)
        {
            foreach (var n in yaml.Children())
            {
                this.languages.Add(new Language(
                        n.GetString("name"),
                        n.GetString("description")));
            }
        }
    }
}