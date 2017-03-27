// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;


    [TestFixture]
    public class BuildSpellListTests
    {
        private BuildSpellList subject;
        [SetUp]
        public void SetUpSubject()
        {
            var wizard = new SpellList();
            wizard.Class = "wizard";
            wizard.Levels.Add(0, new string[] { "cantrip1", "cantrip2" });
            wizard.Levels.Add(1, new string[] { "level 1-1", "level 1-2", 
                "level 1-3", "level 1-4", "level 1-5", "level 1-6",
                "level 1-7", "level 1-8", "level 1-9", "level 1-10"});
            wizard.Levels.Add(2, new string[] { "level 2-1", "level 2-2",
                "level 2-3", "level 2-4", "level 2-5", "level 2-6", "level 2-7"});
            var spellLists = new EntityGateway<SpellList>(new SpellList[] { wizard });
            subject = new BuildSpellList(spellLists);
        }
        
        [Test]
        public void AddsAllSpellsForTheCurrentLevelToTheCharacterIfKnowsAllSpells()
        {
            Assert.Ignore("Not written");
        }

        [Test]
        public void AddsToSpellBookIfWizard()
        {
            var character = new CharacterSheet();
            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 10);
            var cls = new Class();
            cls.Spells.List = "wizard";
            cls.Spells.Known = SpellsKnown.Spellbook;
            cls.Spells.Type = SpellType.Arcane;
            character.SetClass(cls);
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Inventory.Spellbooks.Count(), Is.EqualTo(1));
            var spellbook = character.Inventory.Spellbooks.First();
            Assert.That(spellbook.Spells[0], Is.EquivalentTo(new string [] { "cantrip1", "cantrip2" }));
            Assert.That(spellbook.Spells[1], Has.Length.EqualTo(3));
            Assert.That(character.CasterLevel, Is.EqualTo(1));
        }

        [Test]
        public void ChoosesSpellsKnownBasedOnClassRulesIfSpontaneousCaster()
        {
            Assert.Ignore("Not written");
        }

        [Test]
        public void ClassesWithoutSpellsDoNothing()
        {
            var character = new CharacterSheet();
            character.SetClass(new Class()); 
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.CasterLevel, Is.EqualTo(0));
        }
    }
}