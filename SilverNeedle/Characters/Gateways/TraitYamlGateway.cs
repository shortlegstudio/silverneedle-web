//-----------------------------------------------------------------------
// <copyright file="TraitYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Yaml;

    /// <summary>
    /// Trait yaml gateway provides access to traits in the yaml file
    /// </summary>
    public class TraitYamlGateway : IEntityGateway<Trait>
    {
        /// <summary>
        /// The Trait Data File
        /// </summary>
        private const string TraitDataFile = "Data/traits.yml";

        /// <summary>
        /// The traits available
        /// </summary>
        private IList<Trait> traits;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.TraitYamlGateway"/> class.
        /// </summary>
        public TraitYamlGateway()
        {
            this.LoadFromYaml(FileHelper.OpenYaml(TraitDataFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.TraitYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml to parse.</param>
        public TraitYamlGateway(YamlNodeWrapper yaml)
        {
            this.LoadFromYaml(yaml);
        }

        /// <summary>
        /// Should return all of the traits
        /// </summary>
        /// <returns>Enumerable collection of the entities</returns>
        public System.Collections.Generic.IEnumerable<Trait> All()
        {
            return this.traits;
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <param name="yaml">Yaml node to load from</param>
        private void LoadFromYaml(YamlNodeWrapper yaml)
        {
            this.traits = new List<Trait>();

            foreach (var traitNode in yaml.Children())
            {
                var trait = new Trait();
                trait.Name = traitNode.GetString("name"); 
                ShortLog.Debug("Loading Trait: " + trait.Name);
                trait.Description = traitNode.GetString("description");
                trait.Tags.Add(traitNode.GetCommaStringOptional("tags"));

                // Get Any skill Modifiers if they exist
                var modifiers = traitNode.GetNodeOptional("modifiers");
                if (modifiers != null)
                {
                    var mods = ParseStatModifiersYaml.ParseYaml(modifiers, string.Format("{0} (trait)", trait.Name));
                    foreach (var m in mods)
                    {
                        trait.Modifiers.Add(m);
                    }
                }

                // Get any special abilities
                var abilities = traitNode.GetNodeOptional("special");
                if (abilities != null)
                {
                    foreach (var spec in abilities.Children())
                    {
                        var specialAbility = new SpecialAbility(
                                                 spec.GetString("condition"),
                                                 spec.GetString("type"));
                        trait.SpecialAbilities.Add(specialAbility);
                    }
                }

                this.traits.Add(trait);
            }
        }
    }
}