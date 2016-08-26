//-----------------------------------------------------------------------
// <copyright file="Feat.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SilverNeedle;
    using SilverNeedle.Yaml;

    /// <summary>
    /// Represents a feat ability for a character that allows it to perform
    /// special and advanced abilities
    /// </summary>
    public class Feat : IModifiesStats, IProvidesSpecialAbilities
    {
        /// <summary>
        /// The trait data file.
        /// </summary>
        private const string TraitDataFile = "Data/feats.yml";

        /// <summary>
        /// The feats that are loaded
        /// </summary>
        private static IList<Feat> loadedFeats = new List<Feat>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Feat"/> class.
        /// </summary>
        public Feat()
        {
            this.Modifiers = new List<BasicStatModifier>();
            this.SpecialAbilities = new List<SpecialAbility>();
            this.Prerequisites = new Prerequisites();
            this.Tags = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the feat.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the feat
        /// </summary>
        /// <value>The description of the feat.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this feat.</value>
        public IList<BasicStatModifier> Modifiers { get; private set; }

        public IList<SpecialAbility> SpecialAbilities { get; private set; }

        /// <summary>
        /// Gets the prerequisites.
        /// </summary>
        /// <value>The prerequisites needed to meet requirements for this feat.</value>
        public Prerequisites Prerequisites { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is combat feat.
        /// </summary>
        /// <value><c>true</c> if this instance is combat feat; otherwise, <c>false</c>.</value>
        public bool IsCombatFeat 
        { 
            get { return this.Tags.Contains("combat"); } 
        }

        /// <summary>
        /// Gets a value indicating whether this instance is critical feat.
        /// </summary>
        /// <value><c>true</c> if this instance is critical feat; otherwise, <c>false</c>.</value>
        public bool IsCriticalFeat 
        { 
            get { return this.Tags.Contains("critical"); } 
        }

        /// <summary>
        /// Gets a value indicating whether this instance is item creation.
        /// </summary>
        /// <value><c>true</c> if this instance is item creation; otherwise, <c>false</c>.</value>
        public bool IsItemCreation 
        { 
            get { return this.Tags.Contains("itemcreation"); } 
        }

        /// <summary>
        /// Gets or sets the tags for categorizing this feat.
        /// </summary>
        /// <value>The tags for this feat.</value>
        public IList<string> Tags { get; set; }

        /// <summary>
        /// Loads all the feats from a YAML node.
        /// </summary>
        /// <returns>The feats stored in the YAML document.</returns>
        /// <param name="yaml">YAML to parse out.</param>
        public static IList<Feat> LoadFromYaml(YamlNodeWrapper yaml)
        {
            // TODO: Refactor to Gateway class
            var feats = new List<Feat>();

            foreach (var featNode in yaml.Children())
            {
                var feat = new Feat();
                feat.Name = featNode.GetString("name"); 
                ShortLog.DebugFormat("Loading Feat: {0}", feat.Name);
                feat.Description = featNode.GetString("description");

                // Get Any skill Modifiers if they exist
                var skills = featNode.GetNodeOptional("modifiers");
                if (skills != null)
                {
                    foreach (var skillAdj in skills.Children())
                    {
                        var skillName = skillAdj.GetString("stat");
                        var modifier = skillAdj.GetInteger("modifier");
                        var type = skillAdj.GetString("type");
                        feat.Modifiers.Add(new BasicStatModifier(
                            skillName,
                            modifier,
                            type,
                            string.Format("{0} (feat)", feat.Name)));
                    }
                }

                // Get Any Prerequisites
                var prereq = featNode.GetNodeOptional("prerequisites");
                if (prereq != null)
                {
                    feat.Prerequisites = new Prerequisites(prereq);
                }

                feat.Tags = featNode.GetCommaStringOptional("tags").ToList();
                feats.Add(feat);
            }

            return feats;
        }

        /// <summary>
        /// Gets a feat that matches the name.
        /// </summary>
        /// <returns>The feat to find.</returns>
        /// <param name="name">Name of the feat.</param>
        public static Feat GetFeat(string name)
        {
            return GetFeats().First(x => x.Name == name); 
        }

        /// <summary>
        /// Gets all of the feats.
        /// </summary>
        /// <returns>The feats that are loaded.</returns>
        public static IList<Feat> GetFeats()
        {
            if (loadedFeats == null || loadedFeats.Count == 0)
            {
                var yaml = FileHelper.OpenYaml(TraitDataFile);
                loadedFeats = LoadFromYaml(yaml);
                ShortLog.Debug("Loaded Traits: " + loadedFeats.Count);
            }

            return loadedFeats;
        }

        /// <summary>
        /// Gets all the qualifying feats for a character.
        /// </summary>
        /// <returns>The qualifying feats.</returns>
        /// <param name="character">Character that is validating.</param>
        public static IEnumerable<Feat> GetQualifyingFeats(CharacterSheet character)
        {
            return GetFeats().Where(x => x.IsQualified(character) && !character.Feats.Contains(x));
        }

        /// <summary>
        /// Gets the qualifying feats that are also in tagged with the specified value.
        /// Allows getting qualifying combat feats for example
        /// </summary>
        /// <returns>The qualifying feats.</returns>
        /// <param name="character">Character to qualify.</param>
        /// <param name="tag">Tags to filter the feats.</param>
        public static IEnumerable<Feat> GetQualifyingFeats(CharacterSheet character, string tag)
        {
            return GetQualifyingFeats(character).Where(x => 
                x.Tags.Contains(tag) || string.IsNullOrEmpty(tag));
        }

        /// <summary>
        /// Sets the feats to use for fetching
        /// </summary>
        /// <param name="feats">Feats to store.</param>
        public static void SetFeats(IList<Feat> feats)
        {
            loadedFeats = feats;
        }

        /// <summary>
        /// Figures out if the character is qualified.
        /// </summary>
        /// <param name="character">Character to validate.</param>
        /// <returns>true if the character is qualified</returns>
        public bool IsQualified(CharacterSheet character)
        {
            return Prerequisites.IsQualified(character);
        }
    }
}