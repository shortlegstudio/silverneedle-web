// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;

    [TestFixture]
    public class AddDomainSpellsTests
    {
        [Fact]
        public void FindsDomainsAssociatedWithCharacterAndAddsThoseSpells()
        {
            var character = new CharacterSheet();
            character.SetClass(new Class());
            var configureAir = new MemoryStore();
            configureAir.SetValue("name", "Air");
            configureAir.SetValue("spells", "air 1, air 2");
            var domain = new Domain(configureAir);
            character.Add(domain);


            var spells = new EntityGateway<Spell>(
                new Spell[] { new Spell("air 1", "evocation"), new Spell("air 2", "evoccation")}
            );
            var configure = new MemoryStore();
            configure.SetValue("casting-ability", "wisdom");

            var addSpells = new AddDomainSpells(configure, spells);
            addSpells.Process(character, new CharacterBuildStrategy());

            var spellCasting = character.Get<SpellCasting>();
            Assert.That(spellCasting.CastingAbility, Is.EqualTo(character.AbilityScores.GetAbility(AbilityScoreTypes.Wisdom)));
            Assert.That(spellCasting.GetAvailableSpells(1), Is.EqualTo(new string[] { "air 1"}));
            Assert.That(spellCasting.GetAvailableSpells(2), Is.EqualTo(new string[] { }));
            Assert.That(spellCasting.GetSpellsPerDay(1), Is.EqualTo(1));
            Assert.That(spellCasting.GetSpellsPerDay(2), Is.EqualTo(0));

            character.SetLevel(4);

            Assert.That(spellCasting.GetSpellsPerDay(2), Is.EqualTo(1));
            Assert.That(spellCasting.GetSpellsPerDay(3), Is.EqualTo(0));
            Assert.That(spellCasting.GetAvailableSpells(2), Is.EqualTo(new string[] { "air 2"}));
        }
    }
}