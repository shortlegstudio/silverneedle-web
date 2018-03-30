// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Settlements
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.Settlements;
    using SilverNeedle.Settlements;

    public class ConstructBuildingTests : RequiresDataFiles
    {
        [Fact]
        public void AddsSomeBuildingsToSettlement()
        {
            var settlement = new Settlement(new SettlementStrategy());
            var step = new ConstructBuildings();
            step.Execute(settlement);
            var buildings = settlement.GetAll<Building>();
            Assert.True(buildings.Count() > 1);
        }
    }
}