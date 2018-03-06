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

    public class SelectDomains : ICharacterDesignStep, IFeatureCommand
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
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            for(int i = 0; i < domainCount; i++)
            {
                var currentDoms = components.GetAll<Domain>();
                var domains = domainsGateway.Where(d => !currentDoms.Contains(d));
                var domain = domains.ChooseOne();
                components.Add(domain);
            }

        }
    }
}