//-----------------------------------------------------------------------
// <copyright file="RaceYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Dice;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Yaml;

    /// <summary>
    /// Race yaml gateway.
    /// </summary>
    public class RaceYamlGateway : IRaceGateway
    {
        /// <summary>
        /// The race data file path.
        /// </summary>
        private const string RaceDataFileType = "race";

        /// <summary>
        /// The races.
        /// </summary>
        private IList<Race> races = new List<Race>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.RaceYamlGateway"/> class.
        /// </summary>
        public RaceYamlGateway()
        {
            var yamlFiles = DatafileLoader.Instance.GetYamlFiles(RaceDataFileType);
            foreach(var y in yamlFiles) {
                this.LoadFromYaml(y);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.RaceYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml data.</param>
        public RaceYamlGateway(YamlNodeWrapper yaml)
        {
            this.LoadFromYaml(yaml);
        }

        /// <summary>
        /// Should return all of this entity types
        /// </summary>
        /// <returns>Enumerable collection of the entities</returns>
        public IEnumerable<Race> All()
        {
            return this.races;
        }

        /// <summary>
        /// Fetches a race by name. Case insensitive search.
        /// Throws exception if race not found.
        /// </summary>
        /// <param name="name">Name of race to find</param>
        /// <returns>Race matching by name</returns>
        public Race Get(string name)
        {
            return this.races.First(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <param name="yaml">Yaml data to parse.</param>
        private void LoadFromYaml(YamlNodeWrapper yaml)
        {
            foreach (var raceNode in yaml.Children())
            {
                var race = new Race();
                race.Name = raceNode.GetString("name"); 
                ShortLog.Debug("Loading Race: " + race.Name);
                race.SizeSetting = (CharacterSize)System.Enum.Parse(typeof(CharacterSize), raceNode.GetString("size"));
                race.HeightRange = DiceStrings.ParseDice(raceNode.GetString("height"));
                race.WeightRange = DiceStrings.ParseDice(raceNode.GetString("weight"));

                var abilities = raceNode.GetNode("abilities");
                foreach (var ability in abilities.ChildrenToDictionary())
                {
                    var modifier = new AbilityScoreAdjustment();
                    modifier.Reason = "Racial Trait";
                    modifier.Modifier = int.Parse(ability.Value);

                    // Special case is races that can choose
                    if (string.Compare(ability.Key, "choose", true) == 0)
                    {
                        modifier.ChooseAny = true;
                    }
                    else
                    {
                        modifier.AbilityName = AbilityScore.GetType(ability.Key);
                    }

                    race.AbilityModifiers.Add(modifier);
                }

                var traits = raceNode.GetNode("traits");
                foreach (var trait in traits.Children())
                {
                    race.Traits.Add(trait.Value);
                }

                var languages = raceNode.GetNode("languages");
                race.KnownLanguages.Add(languages.GetCommaStringOptional("known"));
                race.AvailableLanguages.Add(languages.GetCommaStringOptional("available"));

                // Get Speed
                race.BaseMovementSpeed = raceNode.GetInteger("basemovementspeed");

                this.races.Add(race);
            }
        }
    }
}