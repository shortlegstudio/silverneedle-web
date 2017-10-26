// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class JackOfAllTradesTests
    {
        [Fact]
        public void AtTenthLevelCanUseAllSkills()
        {
            var bard = CharacterTestTemplates.BardyBard().WithSkills(
                new string[] {
                    "Training 1",
                    "Training 2"
                }
            );
            bard.SkillRanks.GetSkill("Training 1").Skill.RequireTraining(true);
            bard.SkillRanks.GetSkill("Training 2").Skill.RequireTraining(true);
            var jack = new JackOfAllTrades();
            bard.Add(jack);

            bard.SetLevel(9);
            Assert.False(bard.SkillRanks.GetSkill("Training 1").AbleToUse);
            Assert.False(bard.SkillRanks.GetSkill("Training 2").AbleToUse);
            bard.SetLevel(10);
            jack.LeveledUp(bard.Components);
            Assert.True(bard.SkillRanks.GetSkill("Training 1").AbleToUse);
            Assert.True(bard.SkillRanks.GetSkill("Training 2").AbleToUse);
        }

        [Fact]
        public void AddLevel16AllSkillsAreClassSkills()
        {
            var bard = CharacterTestTemplates.BardyBard().WithSkills(
                new string[] {
                    "Perception",
                    "Climb"
                });

            var jack = new JackOfAllTrades();
            bard.Add(jack);
            bard.SetLevel(16);
            jack.LeveledUp(bard.Components);
            Assert.True(bard.SkillRanks.GetSkill("Perception").ClassSkill);
            Assert.True(bard.SkillRanks.GetSkill("Climb").ClassSkill);
        }
    }
}