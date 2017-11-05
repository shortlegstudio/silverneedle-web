// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;

    
    public class AddDomainSpellsTests
    {
        [Fact]
        public void FindsDomainsAssociatedWithCharacterAndAddsThoseSpells()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SetClass(new Class());
            var configureAir = new MemoryStore();
            configureAir.SetValue("name", "Air");
            configureAir.SetValue("spells", new string[] { "air 1", "air 2"} );
            var domain = new Domain(configureAir);
            character.Add(domain);


            var spells = EntityGateway<Spell>.LoadFromList(
                new Spell[] { new Spell("air 1", "evocation"), new Spell("air 2", "evoccation")}
            );
            var configure = new MemoryStore();
            configure.SetValue("casting-ability", "wisdom");

            var addSpells = new AddDomainSpells(configure, spells);
            addSpells.ExecuteStep(character);

            var spellCasting = character.Get<DomainSpellCasting>();
            Assert.Equal(spellCasting.CastingAbility, character.AbilityScores.GetAbility(AbilityScoreTypes.Wisdom));
            Assert.Equal(spellCasting.GetAvailableSpells(1), new string[] { "air 1"});
            Assert.Equal(spellCasting.GetAvailableSpells(2), new string[] { });
            Assert.Equal(spellCasting.GetSpellsPerDay(1), 1);
            Assert.Equal(spellCasting.GetSpellsPerDay(2), 0);

            character.SetLevel(4);

            Assert.Equal(spellCasting.GetSpellsPerDay(2), 1);
            Assert.Equal(spellCasting.GetSpellsPerDay(3), 0);
            Assert.Equal(spellCasting.GetAvailableSpells(2), new string[] { "air 2"});
        }

        [Fact]
        public void IfCharacterDoesNotHaveDomainsDoNotAddSpellcasting()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var configure = new MemoryStore();
            configure.SetValue("casting-ability", "wisdom");
            var addDomainSpells = new AddDomainSpells(configure, EntityGateway<Spell>.Empty());

            addDomainSpells.ExecuteStep(bob);
            Assert.Null(bob.Get<DomainSpellCasting>());
        }
    }
}