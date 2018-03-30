// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Settlements
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.Settlements;
    using SilverNeedle.Serialization;
    using SilverNeedle.Settlements;

    public class SettlementDesignerTests : RequiresDataFiles
    {
        [Fact]
        public void PerformsAnyStepsDefinedToTheSettlement()
        {
            var yaml = @"---
name: Designer
steps:
  - step: Tests.Actions.Settlements.SettlementDesignerTests+SettlementStepOne
  - step: Tests.Actions.Settlements.SettlementDesignerTests+SettlementStepTwo";

            var designer = new SettlementDesigner(yaml.ParseYaml());
            var settlement = new Settlement(new SettlementStrategy());

            designer.Execute(settlement);
            Assert.Equal("Designer", designer.Name);
            Assert.Equal(2, settlement.Inhabitants.Count());
        }

        public class SettlementStepOne : ISettlementDesignStep
        {
            public void Execute(Settlement settlement)
            {
                settlement.AddInhabitant(CharacterTestTemplates.Barbarian());
            }
        }

        public class SettlementStepTwo : ISettlementDesignStep
        {
            public void Execute(Settlement settlement)
            {
                settlement.AddInhabitant(CharacterTestTemplates.Barbarian());
            }
        }
    }
}