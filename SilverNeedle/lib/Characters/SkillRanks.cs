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

        private BasicStat BonusSkillPoints = new BasicStat();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.SkillRanks"/> class.
        /// </summary>
        /// <param name="skillList">Skills available</param>
        /// <param name="scores">Ability scores for a baseline.</param>
        public SkillRanks(IEnumerable<Skill> skillList, AbilityScores scores)
        {
            this.skills = new Dictionary<string, CharacterSkill>(StringComparer.OrdinalIgnoreCase);
            this.FillSkills(skillList, scores);
            this.BonusSkillPoints.AddModifier(new AbilityStatModifier(scores.GetAbility(AbilityScoreTypes.Intelligence)));
        }

        /// <summary>
        /// Gets the score in a skill.
        /// </summary>
        /// <returns>The score.</returns>
        /// <param name="skill">Skill to lookup.</param>
        public int GetScore(string skill)
        {
            if(this.skills.ContainsKey(skill))
                return this.skills[skill].Score();

            return int.MinValue;
        }

        /// <summary>
        /// Gets the skill.
        /// </summary>
        /// <returns>The skill.</returns>
        /// <param name="skill">Skill name to lookup.</param>
        public CharacterSkill GetSkill(string skill)
        {
            try 
            {
                return this.skills[skill];
            }
            catch
            {
                ShortLog.ErrorFormat("Cannot find skill: {0}", skill);
                throw;
            }
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
                    ShortLog.DebugFormat("Modifying Skill ({0}) by {1}", a.StatisticName, a.Modifier.ToString());
                    sk.AddModifier(a);
                }
                else if(a.StatisticName == "Skill Points") {
                    BonusSkillPoints.AddModifier(a);
                }
            }
        }

        public void SetClassSkill(string skillName)
        {
            ShortLog.DebugFormat("Setting Class Skill: {0}", skillName);
            switch(skillName.ToLower())
            {
                case "craft":
                case "profession":
                case "perform":
                    var skills = this.skills.Where(x => x.Key.StartsWith(skillName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value);
                    foreach (var craft in skills)
                    {
                        craft.ClassSkill = true;
                    }
                    break;
                default:
                    GetSkill(skillName).ClassSkill = true;
                    break;
            }
        }

        public IEnumerable<CharacterSkill> GetClassSkills()
        {
            return this.skills.Values.Where(x => x.ClassSkill);
        }

        public int BonusSkillPointsPerLevel() 
        {
            return BonusSkillPoints.TotalValue;
        }

        /// <summary>
        /// Fills the skills.
        /// </summary>
        /// <param name="skills">Skills available.</param>
        /// <param name="scores">Scores to base skill ranks on.</param>
        public void FillSkills(IEnumerable<Skill> newSkills, AbilityScores scores)
        {
            foreach (var s in newSkills)
            {
                if(!this.skills.ContainsKey(s.Name))
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
}