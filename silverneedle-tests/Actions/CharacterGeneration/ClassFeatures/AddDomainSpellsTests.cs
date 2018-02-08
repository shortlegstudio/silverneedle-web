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

    
    public class AddDomainSpellsTests : RequiresDataFiles
    {
        [Fact]
        public void FindsDomainsAssociatedWithCharacterAndAddsThoseSpells()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SetClass(new Class());
            var domain = Domain.CreateForTesting("Air", new string[] { "air 1", "air 2"});
            character.Add(domain);

            var configure = new MemoryStore();
            configure.SetValue("casting-ability", "wisdom");

            var addSpells = new AddDomainSpells(configure);
            addSpells.ExecuteStep(character);

            var spellCasting = character.Get<DomainCasting>();
            Assert.NotNull(spellCasting);
        }

        [Fact]
        public void IfCharacterDoesNotHaveDomainsDoNotAddSpellcasting()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var configure = new MemoryStore();
            configure.SetValue("casting-ability", "wisdom");
            var addDomainSpells = new AddDomainSpells(configure);

            addDomainSpells.ExecuteStep(bob);
            Assert.Null(bob.Get<DomainCasting>());
        }
    }
}