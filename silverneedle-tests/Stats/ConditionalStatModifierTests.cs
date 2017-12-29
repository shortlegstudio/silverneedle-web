// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Stats
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ConditionalStatModifierTests
    {
        [Fact]
        public void CanLoadsTheValueStatModifierIfJustBasicValuesAreSpecified()
        {
            var yaml = @"---
name: Will
modifier: 2
modifier-type: racial
condition: spells";

            var modifier = new ConditionalStatModifier(yaml.ParseYaml());
            Assert.Equal("Will", modifier.StatisticName);
            Assert.Equal(2, modifier.Modifier);
            Assert.Equal("racial", modifier.Type);
            Assert.Equal("spells", modifier.Condition);
        }
    }
}