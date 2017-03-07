using NUnit.Framework;
using SilverNeedle.Actions.CharacterGenerator.Abilities;
using SilverNeedle.Characters;


namespace Actions {
	[TestFixture]
	public class AbilityScoreRollerTests {
		[Test]
		public void CreateAverageScores() {
			var roller = new AverageAbilityScoreGenerator ();
			var abilities = new AbilityScores ();
			roller.RandomScores (abilities);
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Strength));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Dexterity));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Constitution));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Intelligence));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Wisdom));
			Assert.AreEqual (10, abilities.GetScore (AbilityScoreTypes.Charisma));

		}
	}
}