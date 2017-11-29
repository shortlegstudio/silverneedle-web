// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class CorruptingTouchTests
    {
        [Fact]
        public void UsesPerDayBasedOnCharismaAndEffectsLastForSorcererLevels()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            var corrupting = new CorruptingTouch();
            sorcerer.Add(corrupting);
            Assert.Equal(6, corrupting.UsesPerDay);
            Assert.Equal(1, corrupting.RoundsDuration);
            sorcerer.SetLevel(10);
            Assert.Equal(5, corrupting.RoundsDuration);
        }
    }
}