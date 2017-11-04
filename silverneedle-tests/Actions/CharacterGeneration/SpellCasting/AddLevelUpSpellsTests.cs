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

    
    public class AddLevelUpSpellsTests
    {
        AddLevelUpSpells subject;
        public AddLevelUpSpellsTests()
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
            subject = new AddLevelUpSpells(spellLists, spells);
        }

        [Fact]
        public void AddAllSpellsIfAccessToNextLevelIsAvailable()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var cls = new Class();
            cls.Spells.List = "wizard";
            character.SetClass(cls);
            var spellCasting = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            character.SetLevel(3);
            character.Add(spellCasting);
            spellCasting.SpellsKnown = SpellsKnown.All;
            spellCasting.SetSpellsPerDay(0, 1);
            spellCasting.SetSpellsPerDay(1, 1);
            spellCasting.SetSpellsPerDay(2, 1);
            spellCasting.AddSpells(0, new Spell[] { new Spell("cantrip1", "evocation") });
            spellCasting.AddSpells(1, new Spell[] { new Spell("level 1-1", "evocation") });

            subject.ExecuteStep(character);

            Assert.Equal(2, spellCasting.GetAvailableSpells(2).Count());
        }

        [Fact]
        public void WorksForMultiClassSpellCasters()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var cls = new Class();
            cls.Spells.List = "wizard";
            character.SetClass(cls);
            character.SetLevel(3);
            var scWizard = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            scWizard.SpellsKnown = SpellsKnown.All;
            scWizard.SetSpellsPerDay(0, 1);
            scWizard.SetSpellsPerDay(1, 1);
            scWizard.SetSpellsPerDay(2, 1);
            scWizard.AddSpells(0, new Spell[] { new Spell("cantrip1", "evocation") });
            scWizard.AddSpells(1, new Spell[] { new Spell("level 1-1", "evocation") });

            var scBard = new DivineCasting(character.Get<ClassLevel>(), "wizard");
            scBard.SpellsKnown = SpellsKnown.All;
            scBard.SetSpellsPerDay(0, 1);
            scBard.SetSpellsPerDay(1, 1);
            scBard.SetSpellsPerDay(2, 1);
            scBard.AddSpells(0, new Spell[] { new Spell("cantrip1", "evocation") });
            scBard.AddSpells(1, new Spell[] { new Spell("level 1-1", "evocation") });

            character.Add(scBard);
            character.Add(scWizard);
            subject.ExecuteStep(character);

            Assert.Equal(2, scWizard.GetAvailableSpells(2).Count());
            Assert.Equal(2, scBard.GetAvailableSpells(2).Count());
        }

        [Fact]
        public void IgnoreDomainSpellCasting()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Add(new DomainSpellCasting(new ClassLevel(new Class())));
            subject.ExecuteStep(character);
            // DOES NOT THROW
        }

    }
}