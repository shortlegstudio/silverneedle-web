// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using Xunit;


    public class SkillRanksPrerequisiteTests : RequiresDataFiles
    {
        [Fact]
        public void FailsIfCharacterDoesNotHaveEnoughRanksInSkill()
        {
            var bob = CharacterTestTemplates.AverageBob().WithSkills();

            var configureFail = new MemoryStore();
            configureFail.SetValue("name", "Climb");
            configureFail.SetValue("minimum", 5);
            var fail = new SkillRankPrerequisite(configureFail);
            Assert.False(fail.IsQualified(bob.Components));
        }

        [Fact]
        public void PassesIfCharacterHasEnoughRanksInSkill()
        {

            var bob = CharacterTestTemplates.AverageBob().WithSkills();
            bob.SkillRanks.GetSkill("Perception").AddRank();
            bob.SkillRanks.GetSkill("Perception").AddRank();
            bob.SkillRanks.GetSkill("Perception").AddRank();

            var configurePass = new MemoryStore();
            configurePass.SetValue("name", "Perception");
            configurePass.SetValue("minimum", 3);
            var pass = new SkillRankPrerequisite(configurePass);
            Assert.True(pass.IsQualified(bob.Components));
        }

        [Fact]
        public void IfSkillCannotBeFoundThrowException()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var configure = new MemoryStore();
            configure.SetValue("name", "Some Missing Skill");
            configure.SetValue("minimum", 10);
            var thrw = new SkillRankPrerequisite(configure);
            Assert.Throws(typeof(StatisticNotFoundException), () => thrw.IsQualified(bob.Components));
        }
    }
    
}