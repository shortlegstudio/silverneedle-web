// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class AddBonusFeatTokenTests
    {
        [Fact]
        public void AddsATokenWithAvailableOptions()
        {
            var configuration = new MemoryStore();
            configuration.SetValue("options", new string[] { "combat", "critical" });
            var addToken = new AddBonusFeatToken(configuration);

            var bob = CharacterTestTemplates.AverageBob();
            addToken.ExecuteStep(bob, new CharacterBuildStrategy());

            var token = bob.FeatTokens[0];
            Assert.Contains("combat", token.Tags);
            Assert.Contains("critical", token.Tags);
        }
    }
}