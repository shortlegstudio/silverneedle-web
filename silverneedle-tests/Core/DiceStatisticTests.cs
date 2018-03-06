// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Core
{
    using SilverNeedle;
    using SilverNeedle.Serialization;
    using Xunit;

    public class DiceStatisticTests
    {
        [Fact]
        public void DiceStatisticsCanParseADiceStringForTheirBasicStats()
        {
            var yaml = @"---
name: Damage
dice: 1d8";
            var diceStat = new DiceStatistic(yaml.ParseYaml());
            Assert.Equal("1d8", diceStat.Dice.ToString());
            Assert.Equal("Damage", diceStat.Name);
            Assert.Equal("1d8", diceStat.DisplayString());
        }

        [Fact]
        public void DiceStatisticsCanHaveModifiersAddedToThemToAddDiceToTheCup()
        {
            var yaml = @"---
name: Damage
dice: 1d8";
            var diceStat = new DiceStatistic(yaml.ParseYaml());

            var mod = @"---
name: Damage
dice: 1d6";
            var modifier = new AddDiceModifier(mod.ParseYaml());
            diceStat.AddModifier(modifier);
            Assert.Equal("1d8+1d6", diceStat.DisplayString());
        }

        [Fact]
        public void ValueModifiersChangeTheModifierValue()
        {
            var yaml = @"---
name: Damage
dice: 1d8";
            var diceStat = new DiceStatistic(yaml.ParseYaml());
            var mod = new ValueStatModifier("Damage", 6, "modifier");
            diceStat.AddModifier(mod);
            Assert.Equal("1d8+6", diceStat.DisplayString());
        }
    }
}
