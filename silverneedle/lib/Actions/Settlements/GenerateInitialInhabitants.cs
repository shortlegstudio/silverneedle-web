// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Settlements;

    public class GenerateInitialInhabitants : ISettlementDesignStep
    {
        
        public void Execute(Settlement settlement)
        {
            var strategy = settlement.Get<SettlementStrategy>();
            int target = Randomly.Range(strategy.MinimumPopulation, strategy.MaximumPopulation);
            for(int count = 0; count < target; count++)
            {
                var inhabitantStrategy = GatewayProvider.Find<CharacterStrategy>("inhabitant");
                var designer = GatewayProvider.Find<CharacterDesigner>(inhabitantStrategy.Designer);
                var inhabitant = new CharacterSheet(inhabitantStrategy);
                designer.ExecuteStep(inhabitant);
                settlement.AddInhabitant(inhabitant);
            }
        }
    }
}