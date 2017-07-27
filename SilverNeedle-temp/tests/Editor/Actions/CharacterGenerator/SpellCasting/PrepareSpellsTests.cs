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

        [Fact]
        public void NonCastersDoNothing()
        {
            var character = new CharacterSheet();
            var prepareSpells = new PrepareSpells();
            prepareSpells.Process(character, new CharacterBuildStrategy());
        }
        [Fact]
        public void SelectsUniqueListOfSpellsDependingOnAvailableSlots()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            character.SetClass(cls);
            var spellCasting = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>(), "wizard");
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

        [Fact]
        public void PreparesSpellsFromMultipleSpellCastingClassesIfAvailable()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            character.SetClass(cls);
            var scWizard = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>(), "wizard");
            var scCleric = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>(), "cleric");
            scWizard.SpellsKnown = SpellsKnown.All;
            scWizard.AddSpells(0, new Spell[] { new Spell("cantrip1", "conjuration"), new Spell("cantrip2", "evocation"), new Spell("cantrip3", "transmutation"), new Spell("cantrip4", "evocation") });
            scWizard.SetSpellsPerDay(0, 3);
            scCleric.SpellsKnown = SpellsKnown.All;
            scCleric.AddSpells(0, new Spell[] { new Spell("orison1", "conjuration"), new Spell("orison2", "evocation"), new Spell("cantrip3", "transmutation"), new Spell("cantrip4", "evocation") });
            scCleric.SetSpellsPerDay(0, 3);
            character.Add(scWizard);
            character.Add(scCleric);

            var prepSpells = new PrepareSpells();
            prepSpells.Process(character, new CharacterBuildStrategy());
            Assert.That(scWizard.GetPreparedSpells(0).Length, Is.EqualTo(3));
            Assert.That(scCleric.GetPreparedSpells(0).Length, Is.EqualTo(3));

        }

        [Fact]
        public void SetAllSpellsAsPreparedIfASpontaneousCaster()
        {
            Assert.Ignore();
        }
    }
}