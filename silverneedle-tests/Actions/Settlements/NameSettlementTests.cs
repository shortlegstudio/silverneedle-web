// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Settlements
{
    using Xunit;
    using SilverNeedle.Actions.Settlements;
    using SilverNeedle.Settlements;

    public class NameSettlementTests : RequiresDataFiles
    {
        [Fact]
        public void PicksANameForTheSettlement()
        {
            var settlement = new Settlement();
            var name = new NameSettlement();
            name.Execute(settlement);

            Assert.NotNull(settlement.Name);
        }

    }
}