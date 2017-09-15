// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;

    public class AddDomainSpells : ICharacterDesignStep
    {
        private EntityGateway<Spell> spellGateway;
        private string castingAbility;

        public AddDomainSpells(IObjectStore configuration)
        {
            spellGateway = GatewayProvider.Get<Spell>();
            castingAbility = configuration.GetString("casting-ability");
        }

        public AddDomainSpells(IObjectStore configuration, EntityGateway<Spell> spellGateway)
        {
            this.spellGateway = spellGateway;
            castingAbility = configuration.GetString("casting-ability");
        }
        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var domains = character.GetAll<Domain>();
            if(domains.Empty())
                return;

            var domainSpells = new DomainSpellCasting(character.Get<Inventory>(), character.Get<ClassLevel>());
            domainSpells.SetCastingAbility(character.AbilityScores.GetAbility(castingAbility));
            foreach(var d in domains)
            {
                for(int i = 0; i < d.Spells.Length; i++)
                {
                    //Domain spells start at level 1
                    domainSpells.AddSpells(i + 1, 
                        new Spell[] { spellGateway.Find(d.Spells[i]) }
                    );
                }
            }
            character.Add(domainSpells);
        }
    }
}