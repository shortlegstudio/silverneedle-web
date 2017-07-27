// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class AbilityScoreCostsTests
    {
        AbilityScoreCosts subject;
        [SetUp]
        public void Configure()
        {
            subject = new AbilityScoreCosts();
            subject.PointCosts.Add(7, -4);
            subject.PointCosts.Add(8, -2);
            subject.PointCosts.Add(9, -1);
            subject.PointCosts.Add(10, 0);
            subject.PointCosts.Add(11, 1);
            subject.PointCosts.Add(12, 2);
            subject.PointCosts.Add(13, 3);
            subject.PointCosts.Add(14, 5);
            subject.PointCosts.Add(15, 7);

        }

        [Fact]
        public void ReturnsTheScoresThatAreNegativeCosts()
        {
            Assert.That(subject.NegativeCosts, Is.EquivalentTo(new int[] { 7, 8, 9}));
        }

        [Fact]
        public void PositiveCostsReturnValuesThatAreGreaterThanZero()
        {
            Assert.That(subject.PositiveCosts, Is.EquivalentTo(new int[] { 11, 12, 13, 14, 15 }));
        }

        [Fact]
        public void ZeroCostsReturnsASingleValueThatCostsNothing()
        {
            Assert.That(subject.ZeroCost, Is.EqualTo(10));
        }

        [Fact]
        public void GetNearestValueReturnsTheHighestWithoutGoingOver()
        {
            Assert.That(subject.ClosestValue(6), Is.EqualTo(14));
        }
    }
}