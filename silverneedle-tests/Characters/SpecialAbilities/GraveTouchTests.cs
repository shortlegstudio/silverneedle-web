// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class GraveTouchTests
    {
        private GraveTouch touch;
        private CharacterSheet wizard;
        public GraveTouchTests()
        {
            wizard = CharacterTestTemplates.Wizard();
            var configuration = new MemoryStore();
            configuration.SetValue("base-ability", "intelligence");
            touch = new GraveTouch(configuration);
            wizard.Add(touch);
        }
        [Fact]
        public void UsesPerDayBasedOnBaseAbility()
        {
            Assert.Equal(3, touch.UsesPerDay);
            wizard.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            Assert.Equal(6, touch.UsesPerDay);
        }

        [Fact]
        public void RoundsDurationIsBasedOnSourceLevels()
        {
            Assert.Equal(1, touch.RoundsDuration);
            wizard.SetLevel(10);
            Assert.Equal(5, touch.RoundsDuration);
        }
    }
}