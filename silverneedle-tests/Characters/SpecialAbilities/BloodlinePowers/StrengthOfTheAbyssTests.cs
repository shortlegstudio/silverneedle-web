// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class StrengthOfTheAbyssTests
    {
        [Fact]
        public void IncreasesStrengthAndGrowsWithLevels()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.Add(new StrengthOfTheAbyss());
            Assert.Equal(12, sorcerer.AbilityScores.GetScore(AbilityScoreTypes.Strength));
            sorcerer.SetLevel(13);
            Assert.Equal(14, sorcerer.AbilityScores.GetScore(AbilityScoreTypes.Strength));
            sorcerer.SetLevel(17);
            Assert.Equal(16, sorcerer.AbilityScores.GetScore(AbilityScoreTypes.Strength));
        }
    }

}