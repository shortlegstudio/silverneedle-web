// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Feats
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Feats;
    using SilverNeedle.Utility;

    public class SkillFocusTests
    {
        [Fact]
        public void SelectsASkillBasedOnStrategyForSkillsToProvideABonusTo()
        {
            var generic = CharacterTestTemplates.AverageBob();
            generic.Get<CharacterStrategy>().FavoredSkills.AddEntry("Swim", 1000);
            var skillFocus = SkillFocus.CreateForTesting();
            generic.Add(skillFocus);

            Assert.Equal(3, generic.SkillRanks.GetScore("Swim"));
        }

        [Fact]
        public void TracksTheSkillThatWasSelected()
        {
            var generic = CharacterTestTemplates.AverageBob();
            var skillFocus = SkillFocus.CreateForTesting();
            Assert.Null(skillFocus.CharacterSkill);
            generic.Add(skillFocus);
            Assert.NotNull(skillFocus.CharacterSkill);
        }

        [Fact]
        public void AtTenRanksProvideABonusOfSix()
        {
            var generic = CharacterTestTemplates.AverageBob();
            generic.Get<CharacterStrategy>().FavoredSkills.AddEntry("Swim", 1000);

            var skillFocus = SkillFocus.CreateForTesting();
            generic.Add(skillFocus);

            Repeat.Times(10,
                generic.SkillRanks.GetSkill("Swim").AddRank
            );

            Assert.Equal(16, generic.SkillRanks.GetScore("Swim"));
        }

        [Fact]
        public void IfNoPreferredSkillsAvailableJustChooseOne()
        {

            var generic = CharacterTestTemplates.AverageBob();
            var skillFocus = SkillFocus.CreateForTesting();
            generic.Add(skillFocus);
            Assert.NotNull(skillFocus.CharacterSkill);
            Assert.Equal(3, skillFocus.CharacterSkill.Score());
        }

        [Theory]
        [Repeat(100)]
        public void AddingASecondSkillFocusSelectsADifferentSkill()
        {
            var genericCharacter = CharacterTestTemplates.AverageBob();
            var skillFocus = SkillFocus.CreateForTesting();
            genericCharacter.Add(skillFocus);
            var skillFocus2 = SkillFocus.CreateForTesting();
            genericCharacter.Add(skillFocus2);
            Assert.NotEqual(skillFocus.CharacterSkill, skillFocus2.CharacterSkill);
        }

        [Fact]
        public void SkillFocusThatHasSkillSelectedAlwaysPicksThatSkill()
        {
            var skillFocus = SkillFocus.CreateForTesting();
            skillFocus.SetSkillFocus("Climb");
            var genericCharacter = CharacterTestTemplates.AverageBob();
            genericCharacter.Add(skillFocus);
            Assert.Equal(3, genericCharacter.SkillRanks.GetScore("Climb"));
        }


        [Fact]
        public void CopyingSpecificSkillFocusRetainsSkill()
        {
            var skillFocus = SkillFocus.CreateForTesting();
            skillFocus.SetSkillFocus("Climb");
            var copy = skillFocus.Copy() as SkillFocus;
            Assert.Equal("Climb", copy.SkillName);
            Assert.Equal("Skill Focus(Climb)", copy.Name);
        }
    }
}