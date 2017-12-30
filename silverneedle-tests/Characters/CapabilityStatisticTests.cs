// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class CapabilityStatisticTests
    {
        [Fact]
        public void SetsTheDisplayStringToTheNamePlusTheTotalModifier()
        {
            var yaml = @"---
name: Bravery
base-value: 3";
            var capa = new CapabilityStatistic(yaml.ParseYaml());
            Assert.Equal("Bravery +3", capa.DisplayString());
        }
    }
}