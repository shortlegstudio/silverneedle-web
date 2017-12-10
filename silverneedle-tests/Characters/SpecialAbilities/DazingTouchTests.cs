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

    public class DazingTouchTests
    {
        [Fact]
        public void TimesPerDayBasedOnIntelligence()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var configuration = new MemoryStore();
            configuration.SetValue("base-ability", "intelligence");
            var dazing = new DazingTouch(configuration);
            wizard.Add(dazing);
            Assert.Equal(3, dazing.UsesPerDay);
            wizard.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            Assert.Equal(6, dazing.UsesPerDay);
        }
    }
}