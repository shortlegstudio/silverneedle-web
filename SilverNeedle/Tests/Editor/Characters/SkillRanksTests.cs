using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Mechanics.CharacterGenerator;
using System.Collections.Generic;
using System.Linq;
using SilverNeedle;

namespace Characters {

	[TestFixture]
	public class SkillRanksTests {
		List<Skill> _skillList;
		AbilityScores _abilityScores;

		SkillRanks Subject;

		[SetUp]
		public void SetupCharacter() {
			_skillList = new List<Skill> ();
			_skillList.Add (new Skill ("Climb", AbilityScoreTypes.Strength, false));
			_skillList.Add (new Skill ("Disable Device", AbilityScoreTypes.Dexterity, true));
			_skillList.Add (new Skill ("Stealth", AbilityScoreTypes.Dexterity, false));

			_abilityScores = new AbilityScores ();
			_abilityScores.SetScore (AbilityScoreTypes.Strength, 14);
			_abilityScores.SetScore (AbilityScoreTypes.Dexterity, 12);

			Subject = new SkillRanks (_skillList, _abilityScores);

		}

		[Test]
		public void SkillRanksLoadsAllTheSkills() {
			Assert.AreEqual (2, Subject.GetScore ("Climb"));
			Assert.AreEqual (int.MinValue, Subject.GetScore ("Disable Device"));
		}

		[Test]
		public void CanProcessASkillModifierForModifyingSkills() {
			Subject.ProcessModifier (new MockMod ());
			Assert.AreEqual (5, Subject.GetScore ("Climb"));
		}

		[Test]
		public void ReturnsAListOfSkillsThatHaveRanks() {
			Subject.GetSkill ("Climb").AddRank ();
			var list = Subject.GetRankedSkills ().ToList();
			Assert.AreEqual (1, list.Count);
			Assert.AreEqual ("Climb", list [0].Name);
		}

		[Test]
		public void IfSkillDoesNotExistWhenProcessingAModifierJustLogIt() {
			var ranks = new SkillRanks (new List<Skill> (), new AbilityScores ());
			ranks.ProcessModifier (new MockMod ());
			//Should not throw exception
		}

		class MockMod : IModifiesStats {
			public IList<BasicStatModifier> Modifiers { get; set;  }

			public MockMod() {
				Modifiers = new List<BasicStatModifier>();
				Modifiers.Add(new BasicStatModifier("Climb", 3, "Cause", "Climb"));
            }
		}
	}
}
