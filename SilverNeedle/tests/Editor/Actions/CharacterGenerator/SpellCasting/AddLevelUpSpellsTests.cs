// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.SpellCasting
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

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
            wizard.Levels.Add(1, new string[] { "level 1-1", "level 1-2", 
                "level 1-3", "level 1-4", "level 1-5", "level 1-6",
                "level 1-7", "level 1-8", "level 1-9", "level 1-10"});
            wizard.Levels.Add(2, new string[] { "level 2-1", "level 2-2",
                "level 2-3", "level 2-4", "level 2-5", "level 2-6", "level 2-7"});
            var spellLists = new EntityGateway<SpellList>(new SpellList[] { wizard });
            subject = new AddLevelUpSpells(spellLists);
        }

        [Test]
        public void AddAllSpellsIfAccessToNextLevelIsAvailable()
        {
            var character = new CharacterSheet();
            character.Class.Spells.List = "wizard";
            var spellCasting = character.SpellCasting;
            spellCasting.SpellsKnown = SpellsKnown.All;
            spellCasting.CasterLevel = 3;
            spellCasting.SetSpellsPerDay(0, 1);
            spellCasting.SetSpellsPerDay(1, 1);
            spellCasting.SetSpellsPerDay(2, 1);
            spellCasting.AddSpells(0, new string[] {"cantrip1"});
            spellCasting.AddSpells(1, new string[] {"level 1-1"});

            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(spellCasting.GetAvailableSpells(2).Length, Is.EqualTo(7));
        }
    }
}