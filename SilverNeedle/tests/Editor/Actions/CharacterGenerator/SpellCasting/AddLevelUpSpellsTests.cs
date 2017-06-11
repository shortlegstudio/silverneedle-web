// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.SpellCasting
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class AddLevelUpSpellsTests
    {
        AddLevelUpSpells subject;
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
            subject = new AddLevelUpSpells(spellLists, spells);
        }

        [Test]
        public void AddAllSpellsIfAccessToNextLevelIsAvailable()
        {
            var character = new CharacterSheet();
            var cls = new Class();
            cls.Spells.List = "wizard";
            character.SetClass(cls);
            var spellCasting = new SpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>());
            character.SetLevel(3);
            character.Add(spellCasting);
            spellCasting.SpellsKnown = SpellsKnown.All;
            spellCasting.SetSpellsPerDay(0, 1);
            spellCasting.SetSpellsPerDay(1, 1);
            spellCasting.SetSpellsPerDay(2, 1);
            spellCasting.AddSpells(0, new Spell[] { new Spell("cantrip1", "evocation") });
            spellCasting.AddSpells(1, new Spell[] { new Spell("level 1-1", "evocation") });

            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(spellCasting.GetAvailableSpells(2).Length, Is.EqualTo(2));
        }

    }
}