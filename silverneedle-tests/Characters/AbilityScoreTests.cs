// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;

    
    public class AbilityScoreTests {

        [Fact]
        public void CalculateModifierScore() {
            var score = new AbilityScore (AbilityScoreTypes.Strength, 18);
            Assert.Equal (4, score.BaseModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 4);
            Assert.Equal (-3, score.BaseModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 11);
            Assert.Equal (0, score.BaseModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 20);
            Assert.Equal (5, score.BaseModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 12);
            Assert.Equal (1, score.BaseModifier);


            score = new AbilityScore (AbilityScoreTypes.Strength, 8);
            Assert.Equal (-1, score.BaseModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 9);
            Assert.Equal (-1, score.BaseModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 6);
            Assert.Equal (-2, score.BaseModifier);
        }
            
        [Fact]
        public void TotalScoreIsTheSumOfAllModifiers() {
            var score = new AbilityScore (AbilityScoreTypes.Strength, 15);
            Assert.Equal (15, score.TotalValue);
        }

        [Fact]
        public void YouCanAddAnAdjustmentToAdjustTheTotals() {
            var score = new AbilityScore (AbilityScoreTypes.Strength, 15);
            var adj = new ValueStatModifier (
                "Strength",
                2,
                "racial",
                "test"
            );

            score.AddModifier (adj);
            Assert.Equal (17, score.TotalValue);
            Assert.Equal (3, score.TotalModifier);
        }
    }
}