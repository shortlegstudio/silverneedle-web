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
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;


    
    public class BuildSpellListTests
    {
        private BuildSpellList subject;
        public BuildSpellListTests()
        {
            var wizard = new SpellList();
            wizard.Class = "wizard";
            wizard.Levels.Add(0, new string[] { "cantrip1", "cantrip2" });
            wizard.Levels.Add(1, new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" });
            wizard.Levels.Add(2, new string[] { "level 2-1", "level 2-2" });
            var spellLists = EntityGateway<SpellList>.LoadWithSingleItem(wizard);
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
            var spells = EntityGateway<Spell>.LoadFromList(spellDefs);

            subject = new BuildSpellList(spellLists, spells);
        }
        
        [Fact]
        public void AddsAllSpellsForTheCurrentLevelToTheCharacterIfKnowsAllSpells()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var cls = new Class();
            cls.Spells.List = "wizard";
            character.SetClass(cls);
            var spellcasting = new SpellCasting(character.Inventory, character.Get<ClassLevel>(), "wizard");
            spellcasting.SpellsKnown = SpellsKnown.All;
            character.Add(spellcasting);
            subject.ExecuteStep(character, new CharacterStrategy());

            Assert.NotStrictEqual(character.Get<SpellCasting>().GetAvailableSpells(0), new string [] { "cantrip1", "cantrip2" });
            Assert.NotStrictEqual(character.Get<SpellCasting>().GetAvailableSpells(1), 
                new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" });
        }


        [Fact(Skip="Spontaneous Casters Not Supported")]
        public void ChoosesSpellsKnownBasedOnClassRulesIfSpontaneousCaster()
        {
        }

        [Fact]
        public void ClassesWithoutSpellsDoNothing()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SetClass(new Class()); 
            subject.ExecuteStep(character, new CharacterStrategy());
            //DOES NOT THROW
        }

        [Fact]
        public void WorksWithMultiClassedCasters()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var scWizard = new SpellCasting(character.Inventory, new ClassLevel(new Class()), "wizard");
            scWizard.SpellsKnown = SpellsKnown.All;
            character.Add(scWizard);
            var scBard = new SpellCasting(character.Inventory, new ClassLevel(new Class()), "wizard");
            scBard.SpellsKnown = SpellsKnown.All;
            character.Add(scBard);
            subject.ExecuteStep(character, new CharacterStrategy());

            Assert.NotStrictEqual(scWizard.GetAvailableSpells(0), new string [] { "cantrip1", "cantrip2" });
            Assert.NotStrictEqual(scWizard.GetAvailableSpells(1), 
                new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" });

            Assert.NotStrictEqual(scBard.GetAvailableSpells(0), new string [] { "cantrip1", "cantrip2" });
            Assert.NotStrictEqual(scBard.GetAvailableSpells(1), 
                new string[] { "level 1-1", "level 1-2", "level 1-3", "level 1-4" });
        }

        [Fact]
        public void IgnoreDomainSpellCastingSinceThatsHandledDifferently()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Add(new DomainSpellCasting(new Inventory(), new ClassLevel(new Class())));
            subject.ExecuteStep(character, new CharacterStrategy()); 
            // DOES NOT THROW
        }
    }
}