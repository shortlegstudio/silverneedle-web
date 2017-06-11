// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.SpellCasting
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
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
            var cls = new Class();
            character.SetClass(cls);
            var spellCasting = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>());
            spellCasting.SpellsKnown = SpellsKnown.All;
            spellCasting.AddSpells(0, new Spell[] { new Spell("cantrip1", "conjuration"), new Spell("cantrip2", "evocation"), new Spell("cantrip3", "transmutation"), new Spell("cantrip4", "evocation") });
            spellCasting.AddSpells(1, new Spell[] { new Spell("level1-1", "evocation"), new Spell("level1-2", "evocation"), new Spell("level1-3", "evocation"), new Spell("level1-4", "transmutation") });
            spellCasting.SetSpellsPerDay(0, 3);
            spellCasting.SetSpellsPerDay(1, 1);
            character.Add(spellCasting);

            var prepareSpells = new PrepareSpells();

            prepareSpells.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Get<SpellCasting>().GetPreparedSpells(0).Length, Is.EqualTo(3));
            Assert.That(character.Get<SpellCasting>().GetPreparedSpells(1).Length, Is.EqualTo(1));
        }

        [Test]
        public void SetAllSpellsAsPreparedIfASpontaneousCaster()
        {
            Assert.Ignore();
        }
    }
}