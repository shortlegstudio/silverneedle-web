// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.SpellCasting
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGenerator.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;

    
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
            Assert.Equal(character.Get<SpellCasting>().GetPreparedSpells(0).Length, 3);
            Assert.Equal(character.Get<SpellCasting>().GetPreparedSpells(1).Length, 1);
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
            Assert.Equal(scWizard.GetPreparedSpells(0).Length, 3);
            Assert.Equal(scCleric.GetPreparedSpells(0).Length, 3);

        }

        [Fact(Skip="Spontaneous Casters not Supported")]
        public void SetAllSpellsAsPreparedIfASpontaneousCaster()
        {
        }
    }
}