// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;

    public class SelectDomains : ICharacterDesignStep
    {
        private int domainCount;
        private EntityGateway<Domain> domainsGateway;

        public SelectDomains(IObjectStore configure)
        {
            domainCount = configure.GetInteger("count");
            domainsGateway = GatewayProvider.Get<Domain>();
        }

        public void ExecuteStep(CharacterSheet character)
        {
            for(int i = 0; i < domainCount; i++)
            {
                var currentDoms = character.GetAll<Domain>();
                var domains = domainsGateway.Where(d => !currentDoms.Contains(d));
                var domain = domains.ChooseOne();
                character.Add(domain);
            }

        }
    }
}