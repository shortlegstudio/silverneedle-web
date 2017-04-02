// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php


namespace Characters {
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class SkillTests {
        [Test]
        public void CraftingSkillsAreConsideredBackgroundSkills()
        {
            var skill = new Skill("Craft (Pottery)", AbilityScoreTypes.Intelligence, false);
            Assert.IsTrue(skill.IsBackgroundSkill);
        }

        [Test]
        public void ProfessionSkillsAreConsideredBackgroundSkills()
        {
            var skill = new Skill("Profession (Fisherman)", AbilityScoreTypes.Wisdom, true);
            Assert.IsTrue(skill.IsBackgroundSkill);						
        }

        [Test]
        public void PerformSkillsAreConsideredBackgroundSkills()
        {
            var skill = new Skill("Perform (Oratory)", AbilityScoreTypes.Charisma, false);
            Assert.IsTrue(skill.IsBackgroundSkill);
        }


        [Test]
        public void LoadsFromObjectStore()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Acro");
            data.SetValue("ability", "Strength");
            data.SetValue("trained", false);
            data.SetValue("description", "Some text");
            var skill = new Skill(data);
            Assert.AreEqual("Acro", skill.Name);
            Assert.AreEqual(AbilityScoreTypes.Strength, skill.Ability);
            Assert.IsFalse(skill.TrainingRequired);
            Assert.AreEqual("Some text", skill.Description);   
        }
    }
}