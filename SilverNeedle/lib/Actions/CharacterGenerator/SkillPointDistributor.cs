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

        public void AssignSkillPoints(CharacterSheet character, DistributionSettings settings)
        {
            // Assign Skill Points based on strategy
            var strategy = settings.PreferredSkills;

            for (int i = 0; i < settings.SkillPointsToAssign; i++)
            {
                var option = strategy.ChooseRandomly();
                var skill = character.GetSkill(option);
                skill.AddRank();
            }
        }

        public class DistributionSettings
        {
            public Class Class { get; set; }
            public int SkillPointsToAssign { get; set; }
            public WeightedOptionTable<string> PreferredSkills { get; set; }

            public DistributionSettings (Class cls, WeightedOptionTable<string> skills)
            {
                this.PreferredSkills = skills;
                this.Class = cls;
            }
        }
    }
}