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
        
        [Fact]
        public void AddsAllSpellsForTheCurrentLevelToTheCharacterIfKnowsAllSpells()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.Spells.List = "wizard";
            character.SetClass(cls);
            var spellcasting = new SpellCasting(character.Inventory, character.Get<ClassLevel>(), "wizard");
            spellcasting.SpellsKnown = SpellsKnown.All;
            character.Add(spellcasting);
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Get<SpellCasting>().GetAvailableSpells(0), Is.EquivalentTo(new string [] { "cantrip1", "cantrip2" }));
            Assert.That(character.Get<SpellCasting>().GetAvailableSpells(1), Is.EquivalentTo(
                new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" }));
        }


        [Fact]
        public void ChoosesSpellsKnownBasedOnClassRulesIfSpontaneousCaster()
        {
            Assert.Ignore("Spontaneous Casters Not Supported");
        }

        [Fact]
        public void ClassesWithoutSpellsDoNothing()
        {
            var character = new CharacterSheet();
            character.SetClass(new Class()); 
            subject.Process(character, new CharacterBuildStrategy());
        }

        [Fact]
        public void WorksWithMultiClassedCasters()
        {
            var character = new CharacterSheet();
            var scWizard = new SpellCasting(character.Inventory, new ClassLevel(new Class()), "wizard");
            scWizard.SpellsKnown = SpellsKnown.All;
            character.Add(scWizard);
            var scBard = new SpellCasting(character.Inventory, new ClassLevel(new Class()), "wizard");
            scBard.SpellsKnown = SpellsKnown.All;
            character.Add(scBard);
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(scWizard.GetAvailableSpells(0), Is.EquivalentTo(new string [] { "cantrip1", "cantrip2" }));
            Assert.That(scWizard.GetAvailableSpells(1), Is.EquivalentTo(
                new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" }));

            Assert.That(scBard.GetAvailableSpells(0), Is.EquivalentTo(new string [] { "cantrip1", "cantrip2" }));
            Assert.That(scBard.GetAvailableSpells(1), Is.EquivalentTo(
                new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" }));
        }

        [Fact]
        public void IgnoreDomainSpellCastingSinceThatsHandledDifferently()
        {
            var character = new CharacterSheet();
            character.Add(new DomainSpellCasting(new Inventory(), new ClassLevel(new Class())));
            Assert.DoesNotThrow(() => { subject.Process(character, new CharacterBuildStrategy()); });
        }
    }
}