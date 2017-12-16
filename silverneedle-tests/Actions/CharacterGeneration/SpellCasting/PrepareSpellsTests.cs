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
            var character = CharacterTestTemplates.Wizard().WithWizardCasting().FillSpellbook();
            var spellCasting = character.Get<ISpellCasting>();

            var prepareSpells = new PrepareSpells();

            prepareSpells.ExecuteStep(character);
            Assert.Equal(3, spellCasting.GetReadySpells(0).Count());
            Assert.Equal(2, spellCasting.GetReadySpells(1).Count());
        }

        [Fact]
        public void PreparesSpellsFromMultipleSpellCastingClassesIfAvailable()
        {
            var character = CharacterTestTemplates.Wizard().WithWizardCasting().WithDivineCasting().FillSpellbook();
            var scWizard = character.Get<WizardCasting>();
            var scCleric = character.Get<DivineCastingNew>();

            var prepSpells = new PrepareSpells();
            prepSpells.ExecuteStep(character);
            Assert.Equal(3, scWizard.GetReadySpells(0).Count());
            Assert.Equal(3, scCleric.GetReadySpells(0).Count());

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