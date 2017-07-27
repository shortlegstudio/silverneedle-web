using NUnit.Framework;
using SilverNeedle.Characters;


namespace Characters {
[TestFixture]
	public class AbilityScoresTests {
		private AbilityScores Subject;

		[SetUp]
		public void Configure() {
			Subject = new AbilityScores ();
		}

		[Fact]
		public void AbilityScoresContainerHasAllTheStatsSetToZero() {
			Assert.AreEqual(0, Subject.GetScore(AbilityScoreTypes.Strength));
			Assert.AreEqual(0, Subject.GetScore(AbilityScoreTypes.Wisdom));
			Assert.AreEqual(0, Subject.GetScore("Intelligence"));
		}

		[Fact]
		public void AbilityScoresCanSetAbilities() {
			Subject.SetScore (AbilityScoreTypes.Charisma, 15);
			Assert.AreEqual (15, Subject.GetScore ("Charisma"));
		}

		[Fact]
		public void YouMayGetTheAbilityModifier() {
			Subject.SetScore (AbilityScoreTypes.Charisma, 12);

			Assert.AreEqual (1, Subject.GetModifier (AbilityScoreTypes.Charisma));
		}

		[Fact]
		public void YouCanCopyOneSetOfAbilityScoresToAnother() {
			var abilityScores = new AbilityScores ();
			var copyFrom = new AbilityScores ();
			foreach (var e in copyFrom.Abilities) {
				e.SetValue (15);
			}

			abilityScores.Copy (copyFrom);

			foreach (var e in abilityScores.Abilities) {
				Assert.AreEqual (e.TotalValue, copyFrom.GetScore (e.Ability));
			}
		}
	}
}