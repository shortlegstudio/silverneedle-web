// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.Abilities
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class PointBuyAbilityScoreGeneratorTests
    {
        [Fact]
        [Repeat(100)]
        public void FifteenGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            var gen = new FifteenPointBuy();
            var scores = gen.GetScores();
            var costTotal = 0;

            Assert.That(scores.Count, Is.EqualTo(6));
            
            foreach(var s in scores)
            {
                Assert.That(s, Is.GreaterThanOrEqualTo(7));
                Assert.That(s, Is.LessThanOrEqualTo(18));
                costTotal += gen.PointCosts.PointCosts[s];
            }
            Assert.That(scores, Has.Some.LessThan(10));
            Assert.That(costTotal, Is.EqualTo(15));
        }

        [Fact]
        [Repeat(100)]
        public void TwentyPointGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            
            var gen = new TwentyPointBuy();
            var scores = gen.GetScores();
            var costTotal = 0;

            string scoreString = string.Join(",", scores);
            Assert.That(scores.Count, Is.EqualTo(6), "Not correct number of scores: " + scoreString);
            
            foreach(var s in scores)
            {
                Assert.That(s, Is.GreaterThanOrEqualTo(7));
                Assert.That(s, Is.LessThanOrEqualTo(18));
                costTotal += gen.PointCosts.PointCosts[s];
            }
            Assert.That(scores, Has.Some.LessThan(10));
            Assert.That(costTotal, Is.EqualTo(20));
        }

        [Fact]
        [Repeat(100)]
        public void TwentyPointGeneratorOnlyHasASingleScoreBelowTen()
        {

            var gen = new TwentyPointBuy();
            var scores = gen.GetScores();
            Assert.That(scores, Has.Exactly(1).LessThan(10));
        }
        
        [Fact]
        [Repeat(100)]
        public void TenGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            var gen = new TenPointBuy();
            var scores = gen.GetScores();
            var costTotal = 0;

            Assert.That(scores.Count, Is.EqualTo(6));
            
            foreach(var s in scores)
            {
                Assert.That(s, Is.GreaterThanOrEqualTo(7));
                Assert.That(s, Is.LessThanOrEqualTo(18));
                costTotal += gen.PointCosts.PointCosts[s];
            }
            Assert.That(scores, Has.Some.LessThan(10));
            Assert.That(costTotal, Is.EqualTo(10));
        }
    }
}