// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class AuraOfDespairTests
    {
        [Fact]
        public void RoundsPerDayBasedOnCharacterLevel()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var aura = new AuraOfDespair();
            wizard.Add(aura);
            Assert.Equal(1, aura.RoundsPerDay);
            wizard.SetLevel(12);
            Assert.Equal(12, aura.RoundsPerDay);
            Assert.Equal("Aura Of Despair (12 rounds/day)", aura.Name);
        }
    }
}