// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Settlements
{
    using SilverNeedle.Actions.Settlements;
    using SilverNeedle.Settlements;
    using Xunit;

    public class GenerateInitialInhabitantsTests : RequiresDataFiles
    {
        [Fact]
        public void GenerateANumberOfInhabitantsBasedOnPopulationSettings()
        {
            var strategy = new SettlementStrategy();
            strategy.MinimumPopulation = 2;
            strategy.MaximumPopulation = 10;
            var settlement = new Settlement(strategy);

            var step = new GenerateInitialInhabitants();
            step.Execute(settlement);

            Assert.True(settlement.Population >= strategy.MinimumPopulation);
            Assert.True(settlement.Population <= strategy.MaximumPopulation);
        }
    }
}