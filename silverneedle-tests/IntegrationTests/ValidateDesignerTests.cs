// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.IntegrationTests
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ValidateDesignerTests : RequiresDataFiles
    {
        [Fact]
        public void LoadAndRunAllDesignersToLevel20()
        {
            var strategies = GatewayProvider.All<CharacterStrategy>();
            foreach(var strat in strategies)
            {
                ShortLog.DebugFormat("Validating Strategy: {0}", strat.Name);
                strat.TargetLevel = 20;
                var gen = GatewayProvider.Find<CharacterDesigner>(strat.Designer);
                var character = new CharacterSheet(strat);
                gen.ExecuteStep(character);
            }
        }
    }
}