// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using SilverNeedle.Names;
    using SilverNeedle.Settlements;
    using SilverNeedle.Serialization;

    public class NameSettlement : ISettlementDesignStep
    {
        EntityGateway<SettlementNames> gateway = GatewayProvider.Get<SettlementNames>();

        public void Execute(Settlement settlement)
        {
            var nameGroup = gateway.ChooseOne();
            var gen = new MarkovNameGenerator(nameGroup.Names, 3);
            settlement.Name = gen.Generate(10);
        }
    }
}