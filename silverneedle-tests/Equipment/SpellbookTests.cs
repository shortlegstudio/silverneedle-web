// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;

    public class SpellbookTests
    {
        [Fact]
        public void ReturnsSpellsAvailableInTheBook()
        {
            var book = new Spellbook();
            book.AddSpells(0, new string[] { "light", "foo" });
            Assert.Equal(new string[] { "light", "foo" }, book.GetSpells(0));
        }

        [Fact]
        public void ReturnsEmptyStringIfSpellLevelIsNotInBook()
        {
            var book = new Spellbook();
            Assert.Equal(new string[] { }, book.GetSpells(0));
        }

        [Fact]
        public void KnowsWhetherTheSpellExistsInTheList()
        {
            var book = new Spellbook();
            book.AddSpells(0, new string[] { "light", "foo" });
            Assert.True(book.ContainsSpell(0, "light"));
        }
    }
}