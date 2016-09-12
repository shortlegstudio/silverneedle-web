//-----------------------------------------------------------------------
// <copyright file="SkillPointGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;

    /// <summary>
    /// Skill point generator.
    /// </summary>
    public class SkillPointDistributor
    {
        /// <summary>
        /// Assigns the skill points randomly.
        /// </summary>
        /// <param name="character">Character to assign skill points to.</param>
        public void AssignSkillPointsRandomly(CharacterSheet character)
        {
            var points = character.GetSkillPointsPerLevel();
            var skillList = character.SkillRanks.GetSkills().ToList();
            for (var x = 0; x < points; x++)
            {
                var skill = skillList.ChooseOne();
                while (skill.Ranks >= character.Level)
                {
                    skill = skillList.ChooseOne();
                }

                skill.AddRank();
            }
        }

        public void AssignSkillPoints(SkillRanks skills, WeightedOptionTable<string> preferredSkills, int skillPoints, int maxLevel)
        {
            int assigned = 0;
            while (assigned < skillPoints)
            {
                if (!preferredSkills.IsEmpty)
                {
                    var option = preferredSkills.ChooseRandomly();
                    var skill = skills.GetSkill(option);
                    
                    if(skill.Ranks < maxLevel)
                    {                
                        skill.AddRank();
                        assigned++;
                    }
                    else
                    {
                        preferredSkills.Disable(option);
                    }
                }
                else
                {
                    // no preferred skills so just pick class skills
                    var skill = skills.GetSkills().Where(x => x.ClassSkill).ToList().ChooseOne();
                    if (skill.Ranks < maxLevel)
                    {
                        skill.AddRank();
                        assigned++;
                    }
                }

            }

            ShortLog.Debug("Ending");
        }
    }
}