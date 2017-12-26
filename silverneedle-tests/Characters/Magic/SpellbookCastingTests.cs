// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SpellbookCastingTests : RequiresDataFiles
    {
        private CharacterSheet wizard;
        private SpellbookCasting casting;
        public SpellbookCastingTests()
        {
            wizard = CharacterTestTemplates.Wizard();
            casting = new SpellbookCasting(spellbookYaml.ParseYaml());
            wizard.Add(casting);
        }
        [Fact]
        public void SpellbookCastersHaveASpellbookInTheirInventory()
        {
            AssertCharacter.IsCarrying<Spellbook>(wizard);
        }

        [Fact]
        public void AddingSpellsToTheSpellbookSetsThemAsSpellsAvailable()
        {
            var book = wizard.Inventory.Spellbooks.First();
            book.AddSpells(0, new string[] { "light", "dancing lights" });
            Assert.Equal(
                new string[] { "light", "dancing lights" }, 
                casting.GetKnownSpells(0)
            );
        }

        [Fact]
        public void ASpellCanBePreparedFromTheSpellBook()
        {
            var book = wizard.Inventory.Spellbooks.First();
            book.AddSpells(0, new string[] { "light", "dancing lights" });
            casting.PrepareSpell(0, "light");
            Assert.Equal(new string[] { "light" }, casting.GetReadySpells(0));
        }

        [Fact]
        public void CanPrepareMultiplesOfTheSameSpell()
        {
            var book = wizard.Inventory.Spellbooks.First();
            book.AddSpells(0, new string[] { "light", "dancing lights" });
            casting.PrepareSpell(0, "light");
            casting.PrepareSpell(0, "light");
            Assert.Equal(new string[] { "light", "light" }, casting.GetReadySpells(0));
        }

        [Fact]
        public void IfSpellNotAvailableInBookThrowException()
        {
            Assert.Throws<CannotPrepareSpellException>(
                () => casting.PrepareSpell(0, "not available")
            );
        }

        [Fact]
        public void MultipleSpellbooksProvideSpellsKnwon()
        {
            var book = wizard.Inventory.Spellbooks.First();
            var newBook = new Spellbook();
            wizard.Inventory.AddGear(newBook);

            book.AddSpells(0, new string[] { "light" });
            newBook.AddSpells(0, new string[] { "dancing lights" });
            Assert.Equal(
                new string[] { "light", "dancing lights" },
                casting.GetKnownSpells(0)
            );
        }

        [Fact]
        public void IfNoSpellsArePreparedForALevelJustReturnEmptyStringArray()
        {
            Assert.Equal(
                new string[] { },
                casting.GetReadySpells(70)
            );
        }

        private string spellbookYaml = @"---
list: sorcerer-wizard
type: arcane
casting-ability: intelligence
known: spellbook
spell-slots:
  1: [3, 1]
  2: [4, 2]
  3: [4, 2, 1]
";
    }
}