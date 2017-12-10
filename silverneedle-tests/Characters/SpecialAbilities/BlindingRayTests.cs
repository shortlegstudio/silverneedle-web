// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class BlindingRayTests
    {
        [Fact]
        public void UsesPerDayBasedOnIntelligence()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var ray = new BlindingRay();
            wizard.Add(ray);
            Assert.Equal(3, ray.UsesPerDay);
            wizard.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            Assert.Equal(6, ray.UsesPerDay);
        }
    }
}