// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Groups;

    public class SettlementBuilder
    {
        public Settlement Create(int population)
        {
            var characterCreators = GatewayProvider.Get<CharacterDesigner>();
            var characterBuilder = characterCreators.Find("create-townsfolk");
            var stratGateway = GatewayProvider.Get<CharacterBuildStrategy>();
            var settlement = new Settlement();
            for(int i = 0; i < population; i++)
            {
                var character = new CharacterSheet();
                var strategy = stratGateway.Find("inhabitant");
                characterBuilder.ExecuteStep(character, strategy);
                settlement.AddInhabitant(character);
            }

            return settlement;
        }
    }
}