// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Spells
{
    using Xunit;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SpellListTests
    {
        [Fact]
        public void SpellListsHaveSpellsForEachLevel()
        {
            var data = new MemoryStore();
            data.SetValue("class", "foo");
            var levels = new MemoryStore();
            levels.SetValue("0", "read stuff, do stuff");
            levels.SetValue("1", "kill, explosion");
            data.SetValue("levels", levels);
            var spellList = new SpellList(data);

            Assert.Equal(spellList.Class, "foo");
            Assert.Equal(spellList.Levels.Count, 2);
            Assert.NotStrictEqual(spellList.Levels[0], new string[] {"read stuff", "do stuff"});
            Assert.NotStrictEqual(spellList.Levels[1], new string[] {"kill", "explosion"});
            Assert.True(spellList.Matches("FOO"));
        }

        [Fact]
        public void SpellListsCanBeBuiltManually()
        {
            var spellList = new SpellList();
            spellList.Add(1, "Cure Light Wounds");
            spellList.Add(4, "Cure Moderate Wounds");
        }

        [Fact]
        public void AddingDuplicateSpellsDoesNotDuplicate()
        {

            var spellList = new SpellList();
            spellList.Add(1, "Cure Light Wounds");
            spellList.Add(1, "Cure Light Wounds");
            spellList.Add(1, "Cure Light Wounds");
            Assert.Equal(new string[] { "Cure Light Wounds"}, spellList.Levels[1]);
        }

        [Fact]
        public void CanReturnASpellsLevelFromTheName()
        {
            var spellList = new SpellList();
            spellList.Add(3, "Fireball");
            Assert.Equal(3, spellList.GetSpellLevel("Fireball"));
        }

        [Fact]
        public void IfSpellIsNotFoundThrowException()
        {
            var spellList = new SpellList();
            Assert.Throws(typeof(SpellNotFoundException), () => spellList.GetSpellLevel("Super Awesome Spell"));
        }

        [Fact]
        public void ReturnEmptyListIfLevelDoesNotExists()
        {
            var spellList = new SpellList();
            AssertExtensions.EquivalentLists(new string[] { }, spellList.GetSpells(0));
        }

    }
}