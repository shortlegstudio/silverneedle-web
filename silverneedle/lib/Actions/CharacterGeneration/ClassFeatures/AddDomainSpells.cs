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

    public class AddDomainSpells : ICharacterDesignStep, IFeatureCommand
    {
        private IObjectStore configuration;

        public AddDomainSpells(IObjectStore configuration)
        {
            this.configuration = configuration;
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var domains = components.GetAll<Domain>();
            if(domains.Empty())
                return;

            var domainSpells = new DomainCasting(configuration);
            components.Add(domainSpells);
        }
    }
}