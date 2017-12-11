// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class TelekineticFistTests
    {
        [Fact]
        public void DamageAndUsesPerDayDeterminedByLevelAndAbility()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var fist = new TelekineticFist();
            wizard.Add(fist);

            Assert.Equal("1d4", fist.Damage.ToString());
            wizard.SetLevel(10);
            Assert.Equal("1d4+5", fist.Damage.ToString());

            wizard.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            Assert.Equal(6, fist.UsesPerDay);
        }
    }
}