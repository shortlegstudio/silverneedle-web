// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Serialization;

    public class AddValuesToBuildStrategyTests
    {
        [Fact]
        public void AddsASingleEntryToStrategy()
        {
            var strat = new CharacterBuildStrategy();
            var bob = CharacterTestTemplates.AverageBob();

            var configure = new MemoryStore();
            configure.SetValue("table-name", new string[] { "value" });
            var step = new AddValuesToBuildStrategy(configure);
            step.ExecuteStep(bob, strat);
            Assert.Equal("value", strat.ChooseOption<string>("table-name"));
        }
    }

}