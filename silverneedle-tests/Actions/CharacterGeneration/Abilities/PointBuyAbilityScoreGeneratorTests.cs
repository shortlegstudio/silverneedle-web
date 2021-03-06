// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.Abilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Abilities;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class PointBuyAbilityScoreGeneratorTests : RequiresDataFiles
    {
        [Theory]
        [Repeat(100)]
        public void FifteenGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            var gen = new FifteenPointBuy();
            var scores = gen.GetScores();
            var costTotal = 0;

            Assert.Equal(scores.Count, 6);
            
            foreach(var s in scores)
            {
                Assert.True(s >= 7);
                Assert.True(s <= 18);
                costTotal += gen.PointCosts.PointCosts[s];
            }
            Assert.True(scores.Any(x => x < 10));
            Assert.Equal(costTotal, 15);
        }

        [Theory]
        [Repeat(100)]
        public void TwentyPointGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            
            var gen = new TwentyPointBuy();
            var scores = gen.GetScores();
            var costTotal = 0;

            string scoreString = string.Join(",", scores);
            Assert.Equal(scores.Count, 6);
            
            foreach(var s in scores)
            {
                Assert.True(s >= 7);
                Assert.True(s <= 18);
                costTotal += gen.PointCosts.PointCosts[s];
            }
            Assert.True(scores.Any(x => x < 10));
            Assert.Equal(costTotal, 20);
        }

        [Theory]
        [Repeat(100)]
        public void TwentyPointGeneratorOnlyHasASingleScoreBelowTen()
        {

            var gen = new TwentyPointBuy();
            var scores = gen.GetScores();
            Assert.Equal(1, scores.Where(score => score < 10).Count());
        }
        
        [Theory]
        [Repeat(100)]
        public void TenGeneratorSpendsAllPointsPossibleToCreateScores()
        {
            var gen = new TenPointBuy();
            var scores = gen.GetScores();
            var costTotal = 0;

            Assert.Equal(scores.Count, 6);
            
            foreach(var s in scores)
            {
                Assert.True(s >= 7);
                Assert.True(s <= 18);
                costTotal += gen.PointCosts.PointCosts[s];
            }
            Assert.True(scores.Any(x => x < 10));
            Assert.Equal(costTotal, 10);
        }
    }
}