// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Stats
{
    using SilverNeedle;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using Xunit;

    public class DiceMaximizeAmountModifierTests
    {
        [Fact]
        public void SetsTheCupToMaximizeItsAmount()
        {
            var yaml = @"---
name: stat";
            var max = new DiceMaximizeAmountModifier(yaml.ParseYaml());
            var diceStat = new DiceStatistic("stat", "10d6");
            diceStat.AddModifier(max);
            Assert.True(diceStat.Dice.MaximizeAmount);
        }
    }
}