// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using SilverNeedle.Actions.NamingThings;
    using SilverNeedle.Names;
    using SilverNeedle.Groups;

    public class SettlementBuilder
    {
        public Settlement Create(int population)
        {
            var characterCreators = GatewayProvider.Get<CharacterCreator>();
            var characterBuilder = characterCreators.All().First();
            var stratGateway = new CharacterBuildGateway();
            var strategy = stratGateway.GetBuild("inhabitant");
            var settlement = new Settlement();
            for(int i = 0; i < population; i++)
            {
                var character = new CharacterSheet();
                characterBuilder.ProcessFirstLevel(character, strategy);
                settlement.AddInhabitant(character);
            }

            return settlement;
        }
    }
}