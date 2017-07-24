// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;

    [TestFixture]
    public class WandTests
    {
        [Test]
        public void WandsRepresentASpecificSpell()
        {
            var spell = new Spell("Cure Light Wounds", "healing");
            var wand = new Wand(spell, 50, 750);
            Assert.That(wand.Name, Is.EqualTo("Wand of Cure Light Wounds"));
            Assert.That(wand.Charges, Is.EqualTo(50));
            Assert.That(wand.Value, Is.EqualTo(750));
        }
    }
}