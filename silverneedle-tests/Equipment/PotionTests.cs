// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;

    public class PotionTests
    {
        [Fact]
        public void PotionsHaveASpellAssociatedWithThem()
        {
            var spell = new Spell("Cure Light Wounds", "healing");
            var potion = new Potion(spell, 350);
            Assert.Equal("Potion of Cure Light Wounds", potion.Name);
            Assert.Equal(spell, potion.Spell);
            Assert.Equal(350, potion.Value);
        }
    }
}