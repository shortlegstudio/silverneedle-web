// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class AbilityScoreTokenTests
    {
        [Fact]
        public void CreatesAnAbilityTokenBasedWithValuesInitialized()
        {
            var yaml = @"---
modifier: 2
modifier-type: racial";

            var token = new AbilityScoreToken(yaml.ParseYaml());
            var modifier = token.CreateAdjustment(AbilityScoreTypes.Charisma);
            Assert.Equal("Charisma", modifier.StatisticName);
            Assert.Equal(2, modifier.Modifier);
            Assert.Equal("racial", modifier.Type);
        }
    }
}