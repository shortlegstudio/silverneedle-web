// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class AbilityScoreTests {

        [Fact]
        public void CalculateModifierScore() {
            var score = new AbilityScore (AbilityScoreTypes.Strength, 18);
            Assert.Equal (4, score.TotalModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 4);
            Assert.Equal (-3, score.TotalModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 11);
            Assert.Equal (0, score.TotalModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 20);
            Assert.Equal (5, score.TotalModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 12);
            Assert.Equal (1, score.TotalModifier);


            score = new AbilityScore (AbilityScoreTypes.Strength, 8);
            Assert.Equal (-1, score.TotalModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 9);
            Assert.Equal (-1, score.TotalModifier);

            score = new AbilityScore (AbilityScoreTypes.Strength, 6);
            Assert.Equal (-2, score.TotalModifier);
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

        [Fact]
        public void AddsStatisticComponentsForScoreAndModifierToComponentBag()
        {
            var strength = new AbilityScore(AbilityScoreTypes.Strength, 16);
            var container = new ComponentContainer();
            container.Add(strength);
            var score = container.FindStat("strength");
            var modifier = container.FindStat("strength-modifier");

            Assert.Equal(16, score.TotalValue);
            Assert.Equal(3, modifier.TotalValue);
        }

        [Fact]
        public void LoadsProperlyFromYamlConfiguration()
        {
            var yaml = @"---
name: Strength
base-value: 0";
            var strength = new AbilityScore(yaml.ParseYaml());
            Assert.Equal(0, strength.TotalValue);
            Assert.Equal(-5, strength.TotalModifier);
            Assert.Equal(AbilityScoreTypes.Strength, strength.Ability);
            Assert.Equal("Strength", strength.Name);
            Assert.NotNull(strength.UniversalStatModifier);
        }
    }
}