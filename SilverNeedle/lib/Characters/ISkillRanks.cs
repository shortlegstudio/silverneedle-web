//-----------------------------------------------------------------------
// <copyright file="ISkillRanks.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Skill Ranks interface
    /// </summary>
    public interface ISkillRanks
    {
        /// <summary>
        /// Gets the score for the skill
        /// </summary>
        /// <returns>The score.</returns>
        /// <param name="skill">Skill to fetch score from</param>
        int GetScore(string skill);

        /// <summary>
        /// Gets the skill details.
        /// </summary>
        /// <returns>The skill.</returns>
        /// <param name="skill">Skill to fetch.</param>
        CharacterSkill GetSkill(string skill);

        /// <summary>
        /// Gets the all skills.
        /// </summary>
        /// <returns>The skills for the character.</returns>
        IEnumerable<CharacterSkill> GetSkills();
    }
}