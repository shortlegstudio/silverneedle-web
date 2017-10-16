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
            var skillFocus = new SkillFocus();
            generic.Add(skillFocus);

            Assert.Equal(3, generic.SkillRanks.GetScore("Swim"));
        }

        [Fact]
        public void AtTenRanksProvideABonusOfSix()
        {
            var generic = CharacterTestTemplates.WithSkills(new string[] { "Climb", "Swim", "Perception" });
            generic.Get<CharacterStrategy>().FavoredSkills.AddEntry("Swim", 1000);

            var skillFocus = new SkillFocus();
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
            var skillFocus = new SkillFocus();
            generic.Add(skillFocus);
            Assert.Equal(3, generic.SkillRanks.GetScore("Climb"));
        }
    }
}