//-----------------------------------------------------------------------
// <copyright file="SkillPointGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    /// <summary>
    /// Skill point generator.
    /// </summary>
    public class SkillPointDistributor : ICharacterBuildStep
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
                var selectedSkill = ChooseSkill(skills, preferredSkills, maxLevel);
                if(selectedSkill != null)
                {
                    selectedSkill.AddRank();
                    assigned++;
                }
            }
        }

        private CharacterSkill ChooseSkill(SkillRanks skills, WeightedOptionTable<string> preferredSkills, int maxLevel)
        {

            if (!preferredSkills.IsEmpty)
            {
                var option = preferredSkills.ChooseRandomly();
                var skill = skills.GetSkill(option);

                if (skill.Ranks < maxLevel)
                {
                    return skill;
                }
                else
                {
                    preferredSkills.Disable(option);
                }
            }
            else
            {
                // no preferred skills so just pick class skills
                var skill = skills.GetSkills().Where(
                    x => x.ClassSkill
                    && x.Ranks < maxLevel
                ).ToList().ChooseOne();

                // Background skills require special attention
                // In general just take one background skill
                if (skill.Skill.IsBackgroundSkill)
                {
                    if (skill.Ranks > 0 || skills.GetSkills().Where(x => x.Skill.IsBackgroundSkill).All(x => x.Ranks == 0))
                    {
                        return skill;                       
                    }
                }
                else
                {
                    return skill;
                }            
            }
            return null;
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AssignSkillPoints(character.SkillRanks, strategy.FavoredSkills, character.GetSkillPointsPerLevel(), character.Level);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AssignSkillPoints(character.SkillRanks, strategy.FavoredSkills, character.GetSkillPointsPerLevel(), character.Level);
        }
    }
}