// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Spells;

    [TestFixture]
    public class PrepareSpellsTests
    {

        [Test]
        public void NonCastersDoNothing()
        {
            var character = new CharacterSheet();
            var prepareSpells = new PrepareSpells();
            prepareSpells.Process(character, new CharacterBuildStrategy());
        }
        [Test]
        public void SelectsUniqueListOfSpellsDependingOnAvailableSlots()
        {
            var character = new CharacterSheet();
            character.SpellCasting.SpellsKnown = SpellsKnown.All;
            character.SpellCasting.AddSpells(0, new string[] { "cantrip1", "cantrip2", "cantrip3", "cantrip4" });
            character.SpellCasting.AddSpells(1, new string[] { "level1-1", "level1-2", "level1-3", "level1-4" });
            character.SpellCasting.SetSpellsPerDay(0, 3);
            character.SpellCasting.SetSpellsPerDay(1, 1);

            var prepareSpells = new PrepareSpells();

            prepareSpells.Process(character, new CharacterBuildStrategy());
            Assert.That(character.SpellCasting.GetPreparedSpells(0).Length, Is.EqualTo(3));
            Assert.That(character.SpellCasting.GetPreparedSpells(1).Length, Is.EqualTo(1));
        }

        [Test]
        public void SetAllSpellsAsPreparedIfASpontaneousCaster()
        {
            Assert.Ignore();
        }
    }
}