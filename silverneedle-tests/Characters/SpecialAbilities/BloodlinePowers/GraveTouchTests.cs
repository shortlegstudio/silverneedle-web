// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class GraveTouchTests
    {
        private GraveTouch touch;
        private CharacterSheet sorcerer;
        public GraveTouchTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            touch = new GraveTouch();
            sorcerer.Add(touch);
        }
        [Fact]
        public void UsesPerDayBasedOnCharisma()
        {
            Assert.Equal(3, touch.UsesPerDay);
            sorcerer.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 16);
            Assert.Equal(6, touch.UsesPerDay);
        }

        [Fact]
        public void RoundsDurationIsBasedOnSorcererLevels()
        {
            Assert.Equal(1, touch.RoundsDuration);
            sorcerer.SetLevel(10);
            Assert.Equal(5, touch.RoundsDuration);
        }
    }
}