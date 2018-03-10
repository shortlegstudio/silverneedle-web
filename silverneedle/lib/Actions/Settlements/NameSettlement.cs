// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using System.Linq;
    using SilverNeedle.Names;
    using SilverNeedle.Settlements;
    using SilverNeedle.Serialization;

    public class NameSettlement : ISettlementDesignStep
    {
        EntityGateway<SettlementNames> gateway = GatewayProvider.Get<SettlementNames>();

        [ObjectStore("maximum-length")]
        public int MaximumLength { get; private set; }

        [ObjectStore("order")]
        public int Order { get; private set; }

        public NameSettlement(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public void Execute(Settlement settlement)
        {
            var nameGroup = gateway.All();
            var names = nameGroup.SelectMany(x => x.Names);
            var gen = new MarkovNameGenerator(names, Order);
            settlement.Name = gen.Generate(MaximumLength);
        }
    }
}