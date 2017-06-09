// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.SpellCasting
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;
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
            wizard.Levels.Add(1, new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" });
            wizard.Levels.Add(2, new string[] { "level 2-1", "level 2-2" });
            var spellLists = new EntityGateway<SpellList>(new SpellList[] { wizard });
            var spellDefs = new Spell[] {
                new Spell("cantrip1", "evocation"),
                new Spell("cantrip2", "conjuration"),
                new Spell("level 1-1", "conjuration"),
                new Spell("level 1-2", "conjuration"),
                new Spell("level 1-3", "conjuration"),
                new Spell("level 1-4", "conjuration"),
                new Spell("level 2-1", "transmutation"),
                new Spell("level 2-2", "evocation")
            };
            var spells = new EntityGateway<Spell>(spellDefs);

            subject = new BuildSpellList(spellLists, spells);
        }
        
        [Test]
        public void AddsAllSpellsForTheCurrentLevelToTheCharacterIfKnowsAllSpells()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.Spells.List = "wizard";
            cls.Spells.Known = SpellsKnown.All;
            character.SetClass(cls);
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Get<SpellCasting>().GetAvailableSpells(0), Is.EquivalentTo(new string [] { "cantrip1", "cantrip2" }));
            Assert.That(character.Get<SpellCasting>().GetAvailableSpells(1), Is.EquivalentTo(
                new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" }));
        }

        [Test]
        public void AddsToSpellBookIfWizard()
        {
            var character = new CharacterSheet();
            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 10);
            var cls = new Class();
            cls.Spells.List = "wizard";
            cls.Spells.Known = SpellsKnown.Spellbook;
            character.SetClass(cls);
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Inventory.Spellbooks.Count(), Is.EqualTo(1));
            var spellbook = character.Inventory.Spellbooks.First();
            Assert.That(spellbook.GetSpells(0), Is.EquivalentTo(new string [] { "cantrip1", "cantrip2" }));
            Assert.That(spellbook.GetSpells(1), Has.Length.EqualTo(3));
            Assert.That(character.Get<SpellCasting>().CasterLevel, Is.EqualTo(1));
        }

        [Test]
        public void ChoosesSpellsKnownBasedOnClassRulesIfSpontaneousCaster()
        {
            Assert.Ignore("Spontaneous Casters Not Supported");
        }

        [Test]
        public void ClassesWithoutSpellsDoNothing()
        {
            var character = new CharacterSheet();
            character.SetClass(new Class()); 
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Get<SpellCasting>().CasterLevel, Is.EqualTo(0));
        }
    }
}