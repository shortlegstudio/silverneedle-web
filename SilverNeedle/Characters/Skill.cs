//-----------------------------------------------------------------------
// <copyright file="Skill.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle;

    /// <summary>
    /// Represents a skill that a character can perform
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Skill"/> class.
        /// </summary>
        /// <param name="name">Name of the skill.</param>
        /// <param name="baseAbility">Base ability for the skill.</param>
        /// <param name="trainingRequired">If set to <c>true</c> training required.</param>
        /// <param name="description">Optional description of the skill</param>
        public Skill(string name, AbilityScoreTypes baseAbility, bool trainingRequired, string description = "")
        {
            this.Name = name;
            this.Ability = baseAbility;
            this.TrainingRequired = trainingRequired;
            this.Description = description;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the base ability for the skill.
        /// </summary>
        /// <value>The ability.</value>
        public AbilityScoreTypes Ability { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SilverNeedle.Characters.Skill"/> training required.
        /// </summary>
        /// <value><c>true</c> if training required; otherwise, <c>false</c>.</value>
        public bool TrainingRequired { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; private set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.Skill"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.Skill"/>.</returns>
        public override string ToString()
        {
            return string.Format(
                "[Skill: Name={0}, Ability={1}, Trained={2}]", 
                this.Name, 
                this.Ability, 
                this.TrainingRequired);
        }
    }
}