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
    public class ChooseNaturesBond : ICharacterDesignStep
    {
        private IEnumerable<Domain> domains;
        public ChooseNaturesBond(IObjectStore configuration) 
        {
            GetAvailableDomains(configuration, GatewayProvider.Get<Domain>());
        }

        public ChooseNaturesBond(IObjectStore configuration, EntityGateway<Domain> domains)
        {
            GetAvailableDomains(configuration, domains);
        }
        public void ExecuteStep(CharacterSheet character)
        {
            character.Add(domains.ChooseOne());
        }


        private void GetAvailableDomains(IObjectStore configuration, EntityGateway<Domain> domainGateway)
        {
            var options = configuration.GetList("domain-options");
            domains = domainGateway.FindAll(options);
        }
    }
}