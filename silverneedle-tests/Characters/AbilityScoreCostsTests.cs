// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class AbilityScoreCostsTests
    {
        AbilityScoreCosts subject;
        public AbilityScoreCostsTests()
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
            Assert.NotStrictEqual(subject.NegativeCosts(), new int[] { 7, 8, 9});
        }

        [Fact]
        public void PositiveCostsReturnValuesThatAreGreaterThanZero()
        {
            Assert.NotStrictEqual(subject.PositiveCosts(), new int[] { 11, 12, 13, 14, 15 });
        }

        [Fact]
        public void ZeroCostsReturnsASingleValueThatCostsNothing()
        {
            Assert.Equal(subject.ZeroCost(), 10);
        }

        [Fact]
        public void GetNearestValueReturnsTheHighestWithoutGoingOver()
        {
            Assert.Equal(subject.ClosestValue(6), 14);
        }
    }
}