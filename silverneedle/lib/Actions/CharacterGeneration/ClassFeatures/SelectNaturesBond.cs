// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SelectNaturesBond : ICharacterDesignStep, ICharacterFeatureCommand
    {
        private IEnumerable<Domain> domains;
        public SelectNaturesBond(IObjectStore configuration) 
        {
            GetAvailableDomains(configuration, GatewayProvider.Get<Domain>());
        }

        public SelectNaturesBond(IObjectStore configuration, EntityGateway<Domain> domains)
        {
            GetAvailableDomains(configuration, domains);
        }

        public void Execute(ComponentContainer components)
        {
            components.Add(domains.ChooseOne());
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }


        private void GetAvailableDomains(IObjectStore configuration, EntityGateway<Domain> domainGateway)
        {
            var options = configuration.GetList("domain-options");
            domains = domainGateway.FindAll(options);
        }
    }
}