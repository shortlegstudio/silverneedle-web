// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BardicPerformances;

    public class BardicPerformanceAbilityTests
    {
        [Fact]
        public void BardCanUseAbilityForFourPlusCharismaModifierRounds()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var performance = new BardicPerformanceAbility();
            bard.Add(performance);
            Assert.Equal(4, performance.RoundsPerDay);
            bard.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(7, performance.RoundsPerDay);
        }

        [Fact]
        public void EachLevelAfterOneAddsTwoToTheRoundsPerDay()
        {

            var bard = CharacterTestTemplates.BardyBard();
            var performance = new BardicPerformanceAbility();
            bard.Add(performance);
            bard.SetLevel(3);
            Assert.Equal(8, performance.RoundsPerDay);
        }

        [Fact]
        public void FindsAnyBardicPerformancesAndCanUtilizeThem()
        {
            var bard = CharacterTestTemplates.BardyBard();
            var performance = new BardicPerformanceAbility();
            bard.Add(performance);
            var countersong = new Countersong();
            bard.Add(countersong);
            AssertExtensions.Contains(countersong, performance.Performances);
            Assert.Equal("Bardic Performance 4 rnds/day (Countersong)", performance.Name);
        }
    }
}