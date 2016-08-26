//-----------------------------------------------------------------------
// <copyright file="SkillYamlGateway.cs" company="Short Leg Studio, LLC">
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
    /// Skill yaml gateway provides access to Skills information via a YAML file
    /// </summary>
    public class SkillYamlGateway : IEntityGateway<Skill>
    {
        /// <summary>
        /// The Skills Data File
        /// </summary>
        private const string SkillDataFile = "Data/skills.yml";

        /// <summary>
        /// The skills that have been loaded
        /// </summary>
        private IList<Skill> skills;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.SkillYamlGateway"/> class.
        /// </summary>
        public SkillYamlGateway()
        {
            this.LoadFromYaml(FileHelper.OpenYaml(SkillDataFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.SkillYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml data to parse.</param>
        public SkillYamlGateway(YamlNodeWrapper yaml)
        { 
            this.LoadFromYaml(yaml);
        }

        /// <summary>
        /// Returns all the loaded skills
        /// </summary>
        /// <returns>Enumerable collection of the entities</returns>
        public IEnumerable<Skill> All()
        {
            return this.skills;
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <param name="yaml">Yaml data to load from</param>
        private void LoadFromYaml(YamlNodeWrapper yaml)
        {
            this.skills = new List<Skill>();

            foreach (var skillNode in yaml.Children())
            {
                var skill = new Skill(
                    skillNode.GetString("name"),
                    AbilityScore.GetType(skillNode.GetString("ability")),
                    skillNode.GetString("trained") == "yes",
                    skillNode.GetString("description"));
                this.skills.Add(skill);
            }
        }
    }
}