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
        [Test]
        public void FifteenGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            
            var data = new MemoryStore();
            data.SetValue("points", 15);
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

        [Test]
        public void TwentyPointGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            
            var data = new MemoryStore();
            data.SetValue("points", 15);
            var gen = new TwentyPointBuy();
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
            Assert.That(costTotal, Is.EqualTo(20));
        }
        
        [Test]
        public void TenGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            
            var data = new MemoryStore();
            data.SetValue("points", 15);
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