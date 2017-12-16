// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Spells
{
    using Xunit;
    using Moq;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SpellListTests
    {
        [Fact]
        public void LoadsConfigurationInformationForSpellLists()
        {
            var configuration = @"---
class: foo
levels:
  0: [read stuff, do stuff]
  1: [kill, explosion]".ParseYaml();
            var spellList = new SpellList(configuration);

            Assert.Equal(spellList.Class, "foo");
            Assert.Equal(1, spellList.GetHighestSpellLevel());
            AssertExtensions.EquivalentLists(new string[] {"read stuff", "do stuff"}, spellList.GetAllSpells(0));
            AssertExtensions.EquivalentLists(new string[] {"kill", "explosion"}, spellList.GetAllSpells(1));
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
            Assert.Equal(new string[] { "Cure Light Wounds"}, spellList.GetAllSpells(1));
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
            AssertExtensions.EquivalentLists(new string[] { }, spellList.GetAllSpells(0));
        }

        [Fact]
        public void FiltersSpellsOutBasedOnCriteria()
        {
            var spellFiltered = new Spell("Fireball", "fire");
            var spellReturned = new Spell("Iceball", "cold");
            var gateway = EntityGateway<Spell>.LoadFromList(new Spell[] { spellFiltered, spellReturned });
            SpellList.SetSpellGateway(gateway);
            var spellList = SpellList.CreateForTesting("wizard");
            spellList.Add(1, "Fireball");
            spellList.Add(1, "Iceball");
            var criteria = new Mock<ISpellCastingRule>();
            criteria.Setup(x => x.CanCastSpell(spellFiltered)).Returns(false);
            criteria.Setup(x => x.CanCastSpell(spellReturned)).Returns(true);
            var rule = criteria.Object;
            

            AssertExtensions.EquivalentLists(
                new string[] { "Iceball" }, 
                spellList.GetSpells(1, new ISpellCastingRule[] { rule })
            );
        }

    }
}