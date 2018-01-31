// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class EnergyAbsorptionTests
    {
        [Fact]
        public void ShouldGrantEnergyAbsorptionEqualToThreeTimesLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var absorp = new EnergyAbsorption();
            wizard.Add(absorp);
            Assert.Equal(3, absorp.Amount);
            wizard.SetLevel(10);
            Assert.Equal(30, absorp.Amount);
            Assert.Equal("Energy Absorption (30 pts/day)", absorp.DisplayString());
        }
    }
}