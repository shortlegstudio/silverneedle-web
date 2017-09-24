// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities;

    public class KiPoolTests
    {
        [Fact]
        public void KiPoolHasPointsBasedOnLevelAndWisdom()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 16);
            monk.SetLevel(6);

            var kiPool = new KiPool();
            monk.Add(kiPool);
            Assert.Equal(6, kiPool.KiPoints.TotalValue);
            Assert.Contains("6 points", kiPool.Name);
        }

        [Fact]
        public void KiPoolLooksForKiStrikeAbilities()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            var kiPool = new KiPool();
            monk.Add(kiPool);
            monk.Add(new KiStrike("magic"));
            monk.Add(new KiStrike("lawful"));
            Assert.Contains("magic", kiPool.Name);
            Assert.Contains("lawful", kiPool.Name);
        }
    }
}