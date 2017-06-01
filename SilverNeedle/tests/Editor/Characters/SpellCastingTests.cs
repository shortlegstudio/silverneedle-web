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

        [Test]
        public void MaxLevelIsSetForNowByTheMaxLevelKnown()
        {
            var spells = new SpellCasting(new Inventory()); 
            spells.SpellsKnown = SpellsKnown.All;
            spells.AddSpells(0, new string[] { "foo", "bar" });
            spells.AddSpells(1, new string[] { "foo", "bar" });
            spells.AddSpells(2, new string[] { "foo", "bar" });

            Assert.That(spells.MaxLevel, Is.EqualTo(2));
        }

        [Test]
        public void CanSpecifyTheNumberOfSpellsPerDay()
        {
            var spells = new SpellCasting(new Inventory());
            spells.SetSpellsPerDay(0, 3);
            spells.SetSpellsPerDay(1, 1);

            Assert.That(spells.GetSpellsPerDay(0), Is.EqualTo(3));
            Assert.That(spells.GetSpellsPerDay(1), Is.EqualTo(1));
            Assert.That(spells.GetSpellsPerDay(2), Is.EqualTo(0));
        }

        [Test]
        public void SpellsCanBePrepared()
        {
            var spells = new SpellCasting(new Inventory());
            spells.AddSpells(0, new string[] { "cantrip1", "cantrip2" });
            spells.PrepareSpells(0, new string[] { "cantrip1" });
            Assert.That(spells.GetPreparedSpells(0), Is.EquivalentTo(new string[] {"cantrip1"}));
        }

        [Test]
        public void CalculatesTheDCBasedOnSpellLevelAndAbility()
        {
            var spells = new SpellCasting(new Inventory());
            var abilityScore = new AbilityScore(AbilityScoreTypes.Intelligence, 18);
            spells.SetCastingAbility(abilityScore);
            Assert.That(spells.GetDifficultyClass(0), Is.EqualTo(14));
            Assert.That(spells.GetDifficultyClass(3), Is.EqualTo(17));
            Assert.That(spells.GetDifficultyClass(9), Is.EqualTo(23));
        }

        [Test]
        public void IfAskedForSpellsPastMaxLevelJustReturnZero()
        {
            var spells = new SpellCasting(new Inventory());
            Assert.That(spells.GetSpellsPerDay(200), Is.EqualTo(0));

        }
    }
}