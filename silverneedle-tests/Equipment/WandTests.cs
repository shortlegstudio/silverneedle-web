// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;

    public class WandTests
    {
        [Fact]
        public void WandsRepresentASpecificSpell()
        {
            var spell = new Spell("Cure Light Wounds", "healing");
            var wand = new Wand(spell, 50, 750);
            Assert.Equal(wand.Name, "Wand of Cure Light Wounds");
            Assert.Equal(wand.Charges, 50);
            Assert.Equal(wand.Value, 750);
        }
    }
}