//-----------------------------------------------------------------------
// <copyright file="SkillPointGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Mechanics.CharacterGenerator
{
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;

    /// <summary>
    /// Skill point generator.
    /// </summary>
    public class SkillPointGenerator
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
    }
}