using NUnit.Framework;
using SilverNeedle.Characters;


namespace Characters {

	[TestFixture]
	public class AbilityScoreTests {

	    [Fact]
	    public void CalculateModifierScore() {
			var score = new AbilityScore (AbilityScoreTypes.Strength, 18);
			Assert.AreEqual (4, score.BaseModifier);

			score = new AbilityScore (AbilityScoreTypes.Strength, 4);
			Assert.AreEqual (-3, score.BaseModifier);

			score = new AbilityScore (AbilityScoreTypes.Strength, 11);
			Assert.AreEqual (0, score.BaseModifier);

			score = new AbilityScore (AbilityScoreTypes.Strength, 20);
			Assert.AreEqual (5, score.BaseModifier);

			score = new AbilityScore (AbilityScoreTypes.Strength, 12);
			Assert.AreEqual (1, score.BaseModifier);


			score = new AbilityScore (AbilityScoreTypes.Strength, 8);
			Assert.AreEqual (-1, score.BaseModifier);

			score = new AbilityScore (AbilityScoreTypes.Strength, 9);
			Assert.AreEqual (-1, score.BaseModifier);

			score = new AbilityScore (AbilityScoreTypes.Strength, 6);
			Assert.AreEqual (-2, score.BaseModifier);
	    }
			
		[Fact]
		public void TotalScoreIsTheSumOfAllModifiers() {
			var score = new AbilityScore (AbilityScoreTypes.Strength, 15);
			Assert.AreEqual (15, score.TotalValue);
		}

		[Fact]
		public void YouCanAddAnAdjustmentToAdjustTheTotals() {
			var score = new AbilityScore (AbilityScoreTypes.Strength, 15);
			var adj = new AbilityScoreAdjustment ();
			adj.AbilityName = AbilityScoreTypes.Strength;
			adj.Modifier = 2;

			score.AddModifier (adj);
			Assert.AreEqual (17, score.TotalValue);
			Assert.AreEqual (3, score.TotalModifier);
		}
	}
}