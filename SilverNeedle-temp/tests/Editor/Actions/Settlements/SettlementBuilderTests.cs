// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Settlements
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.Settlements;

    [TestFixture]
    public class SettlementBuilderTests
    {
        [Fact]
        public void CreatesSettlementsWithCharactersOfSpecificSize()
        {
            var settlementBuilder = new SettlementBuilder();
            var settlement = settlementBuilder.Create(3);
            Assert.AreEqual(3, settlement.Population);
            Assert.AreEqual(3, settlement.Inhabitants.Count());
        }
    }
}