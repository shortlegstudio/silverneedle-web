// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.CustomClassSteps
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.CustomClassSteps;
    using SilverNeedle.Characters;

    
    public class ExpertCustomStepsTests
    {
        [Fact]
        public void SelectsTenSkillsForClassSkills()
        {
            //Add twelve skills
            var skills = new Skill[] {
                new Skill("Skill 1", AbilityScoreTypes.Strength, false),
                new Skill("Skill 2", AbilityScoreTypes.Strength, false),
                new Skill("Skill 3", AbilityScoreTypes.Strength, false),
                new Skill("Skill 4", AbilityScoreTypes.Strength, false),
                new Skill("Skill 5", AbilityScoreTypes.Strength, false),
                new Skill("Skill 6", AbilityScoreTypes.Strength, false),
                new Skill("Skill 7", AbilityScoreTypes.Strength, false),
                new Skill("Skill 9", AbilityScoreTypes.Strength, false),
                new Skill("Skill 10", AbilityScoreTypes.Strength, false),
                new Skill("Skill 11", AbilityScoreTypes.Strength, false),
                new Skill("Skill 12", AbilityScoreTypes.Strength, false)
            };
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SkillRanks.FillSkills(skills);

            var subject = new ExpertCustomSteps();
            subject.ExecuteStep(character, new CharacterStrategy());
            Assert.Equal(character.SkillRanks.GetClassSkills().Count(), 10);
        }
    }
}