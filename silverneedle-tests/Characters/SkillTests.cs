// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php


namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class SkillTests {
        [Fact]
        public void CraftingSkillsAreConsideredBackgroundSkills()
        {
            var skill = new Skill("Craft (Pottery)", AbilityScoreTypes.Intelligence, false);
            Assert.True(skill.IsBackgroundSkill);
        }

        [Fact]
        public void ProfessionSkillsAreConsideredBackgroundSkills()
        {
            var skill = new Skill("Profession (Fisherman)", AbilityScoreTypes.Wisdom, true);
            Assert.True(skill.IsBackgroundSkill);						
        }

        [Fact]
        public void PerformSkillsAreConsideredBackgroundSkills()
        {
            var skill = new Skill("Perform (Oratory)", AbilityScoreTypes.Charisma, false);
            Assert.True(skill.IsBackgroundSkill);
        }


        [Fact]
        public void LoadsFromObjectStore()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Acro");
            data.SetValue("ability", "Strength");
            data.SetValue("trained", false);
            data.SetValue("description", "Some text");
            var skill = new Skill(data);
            Assert.Equal("Acro", skill.Name);
            Assert.Equal(AbilityScoreTypes.Strength, skill.Ability);
            Assert.False(skill.TrainingRequired);
            Assert.Equal("Some text", skill.Description);   
        }

        [Fact]
        public void SkillsDenoteWhetherImpactedByArmor()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Acro");
            data.SetValue("ability", "Strength");
            data.SetValue("trained", false);
            data.SetValue("description", "Some text");
            data.SetValue("armor-check-penalty", "true");
            var skill = new Skill(data);
            Assert.True(skill.UseArmorCheckPenalty);
        }
    }
}