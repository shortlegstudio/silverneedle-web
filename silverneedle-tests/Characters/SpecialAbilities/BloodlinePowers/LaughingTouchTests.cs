// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class LaughingTouchTests
    {
        [Fact]
        public void UsesPerDayBasedOnCharisma()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            var laugh = new LaughingTouch();
            sorcerer.Add(laugh);
            Assert.Equal(6, laugh.UsesPerDay);
        }
    }
}