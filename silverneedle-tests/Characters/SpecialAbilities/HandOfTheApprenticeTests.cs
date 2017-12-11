// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class HandOfTheApprenticeTests
    {
        [Fact]
        public void UsesPerDayDeterminedByIntelligence()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var hand = new HandOfTheApprentice();
            wizard.Add(hand);
            Assert.Equal(3, hand.UsesPerDay);
            wizard.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            Assert.Equal(6, hand.UsesPerDay);
        }
    }
}