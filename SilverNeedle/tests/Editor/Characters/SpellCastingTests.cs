// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;

    [TestFixture]
    public class SpellCastingTests
    {
        [Test]
        public void TracksAvailableSpellsForTheCharacter()
        {
            var spellcasting = new SpellCasting(new Inventory());
            spellcasting.AddSpells(0, new string[] { "cantrip1", "cantrip2" });
            Assert.That(spellcasting.GetAvailableSpells(0), Is.EquivalentTo(new string[] { "cantrip1", "cantrip2" }));
        }

        [Test]
        public void IfDependentOnSpellbookPullsAvailableFromSpellbook()
        {
            var inventory = new Inventory();
            var spellcasting = new SpellCasting(inventory);
            spellcasting.SpellsKnown = SpellsKnown.Spellbook;

            // Make a spellbook
            var spellbook = new Spellbook();
            spellbook.AddSpells(0, new string[] { "cantrip1", "cantrip2" });
            inventory.AddGear(spellbook);

            Assert.That(spellcasting.GetAvailableSpells(0), Is.EquivalentTo(new string[] { "cantrip1", "cantrip2" }));

        }
    }
}