// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class BardicKnowledgeTests
    {
        [Fact]
        public void AddsAMinimumOfOneToKnowledgeSkillsAtFirstLevel()
        {
            var bard = CharacterTestTemplates.BardyBard().WithSkills(new string[] {
            "Knowledge Arcana", 
            "Knowledge Religion",
            "Climb"});
            bard.Add(new BardicKnowledge());
            Assert.Equal(1, bard.SkillRanks.GetScore("Knowledge Arcana"));
            Assert.Equal(1, bard.SkillRanks.GetScore("Knowledge Religion"));
            Assert.Equal(0, bard.SkillRanks.GetScore("Climb"));
        }

        [Fact]
        public void DoesHalfCharacterLevelToKnowledge()
        {

            var bard = CharacterTestTemplates.BardyBard().WithSkills(new string[] { "Knowledge Arcana" });
            bard.Add(new BardicKnowledge());
            bard.SetLevel(10);
            Assert.Equal(5, bard.SkillRanks.GetScore("Knowledge Arcana"));

        }

        [Fact]
        public void ChangesAllKnowledgeSkillsToNotRequireTraining()
        {
            var bard = CharacterTestTemplates.BardyBard().WithSkills(new string[] { "Knowledge Arcana" });
            bard.SkillRanks.GetSkill("Knowledge Arcana").Skill.RequireTraining(true);
            Assert.False(bard.SkillRanks.GetSkill("Knowledge Arcana").AbleToUse);
            bard.Add(new BardicKnowledge());
            Assert.True(bard.SkillRanks.GetSkill("Knowledge Arcana").AbleToUse);

        }
    }
}