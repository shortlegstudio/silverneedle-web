//-----------------------------------------------------------------------
// <copyright file="SkillRanks.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Skill ranks tracker for the character
    /// </summary>
    public class SkillRanks : ISkillRanks, IStatTracker
    {
        /// <summary>
        /// The skills available
        /// </summary>
        private IDictionary<string, CharacterSkill> skills;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.SkillRanks"/> class.
        /// </summary>
        /// <param name="skillList">Skills available</param>
        /// <param name="scores">Ability scores for a baseline.</param>
        public SkillRanks(IEnumerable<Skill> skillList, AbilityScores scores)
        {
            this.skills = new Dictionary<string, CharacterSkill>();

            this.FillSkills(skillList, scores);
        }

        /// <summary>
        /// Gets the score in a skill.
        /// </summary>
        /// <returns>The score.</returns>
        /// <param name="skill">Skill to lookup.</param>
        public int GetScore(string skill)
        {
            return this.skills[skill].Score();
        }

        /// <summary>
        /// Gets the skill.
        /// </summary>
        /// <returns>The skill.</returns>
        /// <param name="skill">Skill name to lookup.</param>
        public CharacterSkill GetSkill(string skill)
        {
            return this.skills[skill];
        }

        /// <summary>
        /// Gets the skills.
        /// </summary>
        /// <returns>The skills.</returns>
        public IEnumerable<CharacterSkill> GetSkills()
        {
            return this.skills.Values;
        }

        /// <summary>
        /// Gets the ranked skills.
        /// </summary>
        /// <returns>The ranked skills.</returns>
        public IEnumerable<CharacterSkill> GetRankedSkills()
        {
            return this.skills.Values.Where(
                x => x.Ranks > 0);
        }

        /// <summary>
        /// The implementing class must handle modifiers to stats under its control
        /// </summary>
        /// <param name="modifier">Modifier for stats</param>
        public void ProcessModifier(IModifiesStats modifier)
        {
            foreach (var a in modifier.Modifiers)
            {
                CharacterSkill sk;
                if (this.skills.TryGetValue(a.StatisticName, out sk))
                {
                    sk.AddModifier(a);
                }
            }
        }

        /// <summary>
        /// Fills the skills.
        /// </summary>
        /// <param name="skills">Skills available.</param>
        /// <param name="scores">Scores to base skill ranks on.</param>
        private void FillSkills(IEnumerable<Skill> skills, AbilityScores scores)
        {
            foreach (var s in skills)
            {
                this.skills.Add(
                    s.Name, 
                    new CharacterSkill(
                        s,
                        scores.GetAbility(s.Ability),
                        false));
            }
        }
    }
}