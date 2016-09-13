using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Actions.CharacterGenerator;
using System.Collections.Generic;
using System.Linq;
using SilverNeedle;

namespace Characters
{

    [TestFixture]
    public class SkillRanksTests
    {
        List<Skill> _skillList;
        AbilityScores _abilityScores;

        SkillRanks Subject;

        [SetUp]
        public void SetupCharacter()
        {
            _skillList = new List<Skill>();
            _skillList.Add(new Skill("Climb", AbilityScoreTypes.Strength, false));
            _skillList.Add(new Skill("Disable Device", AbilityScoreTypes.Dexterity, true));
            _skillList.Add(new Skill("Stealth", AbilityScoreTypes.Dexterity, false));

            _abilityScores = new AbilityScores();
            _abilityScores.SetScore(AbilityScoreTypes.Strength, 14);
            _abilityScores.SetScore(AbilityScoreTypes.Dexterity, 12);

            Subject = new SkillRanks(_skillList, _abilityScores);

        }

        [Test]
        public void SkillRanksLoadsAllTheSkills()
        {
            Assert.AreEqual(2, Subject.GetScore("Climb"));
            Assert.AreEqual(int.MinValue, Subject.GetScore("Disable Device"));
        }

        [Test]
        public void CanProcessASkillModifierForModifyingSkills()
        {
            Subject.ProcessModifier(new MockMod());
            Assert.AreEqual(5, Subject.GetScore("Climb"));
        }

        [Test]
        public void ReturnsAListOfSkillsThatHaveRanks()
        {
            Subject.GetSkill("Climb").AddRank();
            var list = Subject.GetRankedSkills().ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Climb", list[0].Name);
        }

				[Test]
				public void CaseInsensitiveSearching()
				{
						Assert.AreEqual("Climb", Subject.GetSkill("climb").Name);
				}

        [Test]
        public void IfSkillDoesNotExistWhenProcessingAModifierJustLogIt()
        {
            var ranks = new SkillRanks(new List<Skill>(), new AbilityScores());
            ranks.ProcessModifier(new MockMod());
            //Should not throw exception
        }

        [Test]
        public void SettingACraftSkillAsClassSkillSetsAllCraftSkills()
        {
            _skillList.Add(new Skill("Craft (Shoes)", AbilityScoreTypes.Intelligence, false));
            _skillList.Add(new Skill("Craft (Jewelry)", AbilityScoreTypes.Intelligence, false));
            var ranks = new SkillRanks(_skillList, new AbilityScores());
            ranks.SetClassSkill("Craft");
            Assert.IsTrue(ranks.GetSkill("Craft (Shoes)").ClassSkill);
            Assert.IsTrue(ranks.GetSkill("Craft (Jewelry)").ClassSkill);
        }

        [Test]
        public void SettingAProfessionsSkillAsClassSkillSetsAllProfessionsSkills()
        {
            _skillList.Add(new Skill("Profession (Bouncer)", AbilityScoreTypes.Intelligence, false));
            _skillList.Add(new Skill("Profession (Turnip Farmer)", AbilityScoreTypes.Intelligence, false));
            var ranks = new SkillRanks(_skillList, new AbilityScores());
            ranks.SetClassSkill("Profession");
            Assert.IsTrue(ranks.GetSkill("Profession (Bouncer)").ClassSkill);
            Assert.IsTrue(ranks.GetSkill("Profession (Turnip Farmer)").ClassSkill);
        }

        [Test]
        public void SettingAPerformSkillAsClassSkillSetsAllPerformSkills()
        {
            _skillList.Add(new Skill("Perform (Debate)", AbilityScoreTypes.Intelligence, false));
            _skillList.Add(new Skill("Perform (Art)", AbilityScoreTypes.Intelligence, false));
            var ranks = new SkillRanks(_skillList, new AbilityScores());
            ranks.SetClassSkill("Perform");
            Assert.IsTrue(ranks.GetSkill("Perform (Debate)").ClassSkill);
            Assert.IsTrue(ranks.GetSkill("Perform (Art)").ClassSkill);
        }
    
        [Test]
        public void CanFetchAllTheClassSkills()
        {
            Subject.SetClassSkill("Climb");
            var classSkills = Subject.GetClassSkills();
            Assert.AreEqual(1, classSkills.Count());
            Assert.AreEqual("Climb", classSkills.First().Name);
        }
        class MockMod : IModifiesStats
        {
            public IList<BasicStatModifier> Modifiers { get; set; }

            public MockMod()
            {
                Modifiers = new List<BasicStatModifier>();
                Modifiers.Add(new BasicStatModifier("Climb", 3, "Cause", "Climb"));
            }
        }
    }
}
