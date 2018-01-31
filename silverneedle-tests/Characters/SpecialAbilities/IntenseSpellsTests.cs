// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class IntenseSpellsTests
    {
        [Fact]
        public void AddsHalfLevelAtLeastOneDamageToSpells()
        {
            var wizard = CharacterTestTemplates.Wizard();
            var intense = new IntenseSpells();
            wizard.Add(intense);
            Assert.Equal(1, intense.BonusDamage);
            wizard.SetLevel(10);
            Assert.Equal(5, intense.BonusDamage);
            Assert.Equal("Intense Spells (+5 spell damage)", intense.DisplayString());
        }
    }
}