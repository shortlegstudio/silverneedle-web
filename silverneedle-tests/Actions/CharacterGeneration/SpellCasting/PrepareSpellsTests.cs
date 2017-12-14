// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.SpellCasting
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;

    
    public class PrepareSpellsTests
    {

        [Fact]
        public void NonCastersDoNothing()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var prepareSpells = new PrepareSpells();
            prepareSpells.ExecuteStep(character);
        }
        [Fact]
        public void SelectsUniqueListOfSpellsDependingOnAvailableSlots()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var cls = new Class();
            character.SetClass(cls);
            var spellCasting = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            spellCasting.SpellsKnown = SpellsKnown.All;
            spellCasting.AddSpells(0, new Spell[] { new Spell("cantrip1", "conjuration"), new Spell("cantrip2", "evocation"), new Spell("cantrip3", "transmutation"), new Spell("cantrip4", "evocation") });
            spellCasting.AddSpells(1, new Spell[] { new Spell("level1-1", "evocation"), new Spell("level1-2", "evocation"), new Spell("level1-3", "evocation"), new Spell("level1-4", "transmutation") });
            spellCasting.SetSpellsPerDay(0, 3);
            spellCasting.SetSpellsPerDay(1, 1);
            character.Add(spellCasting);

            var prepareSpells = new PrepareSpells();

            prepareSpells.ExecuteStep(character);
            Assert.Equal(3, spellCasting.GetPreparedSpells(0).Count());
            Assert.Equal(1, spellCasting.GetPreparedSpells(1).Count());
        }

        [Fact]
        public void PreparesSpellsFromMultipleSpellCastingClassesIfAvailable()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var cls = new Class();
            character.SetClass(cls);
            var scWizard = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            var scCleric = new DivineCasting(character.Get<ClassLevel>(), "cleric");
            scWizard.SpellsKnown = SpellsKnown.All;
            scWizard.AddSpells(0, new Spell[] { new Spell("cantrip1", "conjuration"), new Spell("cantrip2", "evocation"), new Spell("cantrip3", "transmutation"), new Spell("cantrip4", "evocation") });
            scWizard.SetSpellsPerDay(0, 3);
            scCleric.SpellsKnown = SpellsKnown.All;
            scCleric.AddSpells(0, new Spell[] { new Spell("orison1", "conjuration"), new Spell("orison2", "evocation"), new Spell("cantrip3", "transmutation"), new Spell("cantrip4", "evocation") });
            scCleric.SetSpellsPerDay(0, 3);
            character.Add(scWizard);
            character.Add(scCleric);

            var prepSpells = new PrepareSpells();
            prepSpells.ExecuteStep(character);
            Assert.Equal(3, scWizard.GetPreparedSpells(0).Count());
            Assert.Equal(3, scCleric.GetPreparedSpells(0).Count());

        }

        [Fact]
        public void IfCasterHasNoCantripsShouldStillPrepareLevelOneSpells()
        {
            var character = CharacterTestTemplates.Ranger().WithDivineCastingNoOrisons();
            var casting = character.Get<ISpellCasting>();
            var prepSpells = new PrepareSpells();
            prepSpells.ExecuteStep(character);
            Assert.NotEmpty(casting.GetReadySpells(1));

        }
    }
}