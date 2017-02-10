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
    using SilverNeedle.Utility;

    /// <summary>
    /// Skill yaml gateway provides access to Skills information via a YAML file
    /// </summary>
    public class SkillGateway : IEntityGateway<Skill>
    {
        /// <summary>
        /// The Skills Data File
        /// </summary>
        private const string SkillDataFileType = "skill";

        /// <summary>
        /// The skills that have been loaded
        /// </summary>
        private IList<Skill> skills = new List<Skill>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.SkillGateway"/> class.
        /// </summary>
        public SkillGateway()
        {
            foreach(var y in DatafileLoader.Instance.GetDataFiles(SkillDataFileType)) {
                this.LoadObjects(y);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.SkillYamlGateway"/> class.
        /// </summary>
        /// <param name="dataStore">Yaml data to parse.</param>
        public SkillGateway(IObjectStore dataStore)
        { 
            this.LoadObjects(dataStore);
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
        /// Loads from data store.
        /// </summary>
        /// <param name="dataStore">Data to load from</param>
        private void LoadObjects(IObjectStore dataStore)
        {
            foreach (var skillNode in dataStore.Children)
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