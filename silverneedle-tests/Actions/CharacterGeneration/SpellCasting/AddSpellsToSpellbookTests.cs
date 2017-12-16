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
    using SilverNeedle.Serialization;

    public class AddSpellsToSpellbookTests
    {
        private CharacterSheet wizard;

        public AddSpellsToSpellbookTests()
        {
            var configuration = new MemoryStore();
            configuration.SetValue(
                "spells",
                new string[] { "all", "3" }
            );

            var step = new AddSpellsToSpellbook(configuration);
            wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            step.ExecuteStep(wizard);
            
        }

        [Fact]
        public void ContainsAllZeroLevelSpells()
        {
            var casting = wizard.Get<ISpellCasting>();
            Assert.Equal(
                casting.SpellList.GetAllSpells(0),
                casting.GetKnownSpells(0)
            );
        }

        [Fact]
        public void WillAddTheModifierToNumericSlotsIfSetToTrue()
        {
            var configuration = new MemoryStore();
            configuration.SetValue(
                "spells",
                new string[] { "all", "3" }
            );
            configuration.SetValue("add-modifier", true);

            var step = new AddSpellsToSpellbook(configuration);
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            wizard.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 16);
            step.ExecuteStep(wizard);
            var casting = wizard.Get<ISpellCasting>();
            Assert.Equal(
                6, 
                casting.GetKnownSpells(1).Count()
            );
        }

        [Fact]
        public void ContainsASelectionOfOtherLevelSpells()
        {
            var casting = wizard.Get<ISpellCasting>();
            Assert.Equal(
                3,
                casting.GetKnownSpells(1).Count()
            );
        }
        
        [Fact]
        public void DoNotAddSpellsThatAlreadyExist()
        {

        }
    }
}