// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Settlements
{
    using Xunit;
    using SilverNeedle.Actions.Settlements;
    using SilverNeedle.Serialization;
    using SilverNeedle.Settlements;

    public class NameSettlementTests : RequiresDataFiles
    {
        [Fact]
        public void PicksANameForTheSettlement()
        {
            var settlement = new Settlement(new SettlementStrategy());
            var memstore = new MemoryStore();
            memstore.SetValue("maximum-length", 15);
            memstore.SetValue("order", 2);
            var name = new NameSettlement(memstore);
            name.Execute(settlement);

            Assert.NotNull(settlement.Name);
        }

    }
}