// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Settlements
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.Settlements;

    
    public class SettlementBuilderTests : RequiresDataFiles
    {
        [Fact]
        public void CreatesSettlementsWithCharactersOfSpecificSize()
        {
            var settlementBuilder = new SettlementBuilder();
            var settlement = settlementBuilder.Create(3);
            Assert.Equal(3, settlement.Population);
            Assert.Equal(3, settlement.Inhabitants.Count());
        }
    }
}