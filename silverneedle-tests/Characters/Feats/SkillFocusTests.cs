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
            var generic = CharacterTestTemplates.WithSkills(new string[] { "Climb", "Swim", "Perception" });
            generic.Get<CharacterStrategy>().FavoredSkills.AddEntry("Swim", 1000);
            var skillFocus = SkillFocus.CreateForTesting();
            generic.Add(skillFocus);

            Assert.Equal(3, generic.SkillRanks.GetScore("Swim"));
        }

        [Fact]
        public void TracksTheSkillThatWasSelected()
        {
            var generic = CharacterTestTemplates.WithSkills(new string[] { "Climb" });
            var skillFocus = SkillFocus.CreateForTesting();
            generic.Add(skillFocus);
            Assert.Equal(generic.SkillRanks.GetSkill("Climb"), skillFocus.CharacterSkill);
        }

        [Fact]
        public void AtTenRanksProvideABonusOfSix()
        {
            var generic = CharacterTestTemplates.WithSkills(new string[] { "Climb", "Swim", "Perception" });
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

            var generic = CharacterTestTemplates.WithSkills(new string[] { "Climb" });
            var skillFocus = SkillFocus.CreateForTesting();
            generic.Add(skillFocus);
            Assert.Equal(3, generic.SkillRanks.GetScore("Climb"));
        }

        [Fact]
        public void ShouldStartBeNamedProperlyAndStuff()
        {
            var skillFocus = SkillFocus.CreateForTesting();
            Assert.Equal("Skill Focus", skillFocus.Name);
        }

        [Fact]
        [Repeat(1000)]
        public void AddingASecondSkillFocusSelectsADifferentSkill()
        {
            var genericCharacter = CharacterTestTemplates.WithSkills(new string[] { "Climb", "Swim" });
            var skillFocus = SkillFocus.CreateForTesting();
            genericCharacter.Add(skillFocus);
            var skillFocus2 = SkillFocus.CreateForTesting();
            genericCharacter.Add(skillFocus2);
            Assert.Equal(3, genericCharacter.SkillRanks.GetScore("Climb"));
            Assert.Equal(3, genericCharacter.SkillRanks.GetScore("Swim"));
        }

        public void SkillFocusThatHasSkillSelectedAlwaysPicksThatSkill()
        {
            var skillFocus = SkillFocus.CreateForTesting();
            skillFocus.SetSkillFocus("Climb");
            var genericCharacter = CharacterTestTemplates.WithSkills(new string[] { "Climb", "Swim" });
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