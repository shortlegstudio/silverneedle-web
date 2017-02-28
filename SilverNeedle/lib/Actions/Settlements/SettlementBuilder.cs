// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using SilverNeedle.Characters;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using SilverNeedle.Actions.NamingThings;
    using SilverNeedle.Names;
    using SilverNeedle.Groups;

    public class SettlementBuilder
    {
        private GatewayProvider gateways;
        public SettlementBuilder() 
        {   
            gateways = new GatewayProvider();
        }

        public Settlement Create(int population)
        {
            var characterBuilder = new CharacterGenerator.CharacterBuilder(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageGateway()),
                new RaceSelector(gateways.Races, new TraitGateway()),
                new NameCharacter(new CharacterNamesGateway()),
                new FeatSelector(gateways.Feats),
                new GatewayProvider()
            );

            var settlement = new Settlement();
            for(int i = 0; i < population; i++)
            {
                settlement.AddInhabitant(characterBuilder.GenerateRandomCharacter());
            }

            return settlement;
        }
    }
}