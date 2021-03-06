﻿// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Core
{

    using Xunit;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class BasicStatTests {
        [Fact]
        public void StatsTotalUpAdjustments() 
        {
            var stat = new BasicStat ("TestStat", 10);
            stat.AddModifier (new ValueStatModifier (5));
            Assert.Equal (15, stat.TotalValue);
        }

        [Fact]
        public void StatModifiersCanHaveConditionalModifiers() 
        {
            var stat = new BasicStat("TestStat", 10);
            var mod = new ValueStatModifier("Skill", 5, "bonus");
            mod.Condition = "vs. Giants";
            stat.AddModifier(mod);
            Assert.Equal(10, stat.TotalValue);
            Assert.Equal(15, stat.GetConditionalValue("vs. Giants"));
            Assert.Equal(1, stat.GetConditions().Count());
            Assert.Equal("vs. Giants", stat.GetConditions().First());
        }

        [Fact]
        public void StatsCanHaveAListOfConditionalModifiers() 
        {
            var stat = new BasicStat("TestStat", 10);
            stat.AddModifier(
                new ConditionalStatModifier(new ValueStatModifier("Skill", 3, "bonus"), "vs. Corgis") 
            );
            stat.AddModifier(
                new ConditionalStatModifier(new ValueStatModifier("Skill", 3, "bonus"), "vs. Corgis") 
            );
            stat.AddModifier(
                new ConditionalStatModifier(new ValueStatModifier("Skill", 3, "bonus"), "Trapfinding") 
            );

            Assert.Equal(2, stat.GetConditions().Count());
            Assert.True(stat.GetConditions().Any(x => x == "vs. Corgis"));
            Assert.True(stat.GetConditions().Any(x => x == "Trapfinding"));
        }

        [Fact]
        public void CastingDoesntBreakConditionalModifiers() 
        {
            var stat = new BasicStat("TestStat", 10);
            IValueStatModifier mod = new ConditionalStatModifier(new ValueStatModifier("Attack Bonus", 3, "bonus"), "vs. Thor");
            stat.AddModifier(mod);
            Assert.Equal(1, stat.GetConditions().Count());
            Assert.Equal(10, stat.TotalValue);
            Assert.Equal(13, stat.GetConditionalValue("vs. Thor"));
        }
      
        [Fact]
        public void FormatNiceStringVersionOfStat() 
        {
            var stat = new BasicStat("TestStat", 20);
            IValueStatModifier mod = new ConditionalStatModifier(new ValueStatModifier("Attack Bonus", 3, "bonus"), "vs. Thor");
            stat.AddModifier(mod);
            Assert.Equal("Fight +20 (+23 vs. Thor)", stat.ToString("Fight"));
        }

        [Fact]
        public void AlwaysRoundDownStats() 
        {
            var stat = new BasicStat("TestStat", 2);
            stat.AddModifier(new ValueStatModifier(-1));
            stat.AddModifier(new ValueStatModifier(0.667f));
            Assert.Equal(-1, stat.SumBasicModifiers());
            Assert.Equal(1, stat.TotalValue);
        }

        [Fact]
        public void StatsCanHaveMaximumValuesThatClampTheResult()
        {
            var stat = new BasicStat("Stat 1");
            stat.Maximum = 29;
            var mod = new ValueStatModifier(30);
            stat.AddModifier(mod);
            Assert.Equal(stat.TotalValue, 29);
        }

        [Fact]
        public void StatsCanHaveAMinimumValueThatClampTheResult() 
        {
            var stat = new BasicStat("Stat!");
            stat.Minimum = 3;
            var mod = new ValueStatModifier(-20);
            stat.AddModifier(mod);
            Assert.Equal(stat.TotalValue, 3);
        }

        [Fact]
        public void CanAddMultipleModifiersAtOnce()
        {
            var stat = new BasicStat("Statistic");
            stat.AddModifiers(
                new ValueStatModifier(1),
                new ValueStatModifier(3),
                new ValueStatModifier(-2)
            );
            Assert.Equal(stat.Modifiers.Count(), 3);
        }

        [Fact]
        public void CanLoadStatFromConfiguration()
        {
            var yaml = @"---
name: Stat Name
base-value: 2";
            var stat = new BasicStat(yaml.ParseYaml());
            Assert.Equal("Stat Name", stat.Name);
            Assert.Equal(2, stat.BaseValue);
            Assert.Equal(int.MinValue, stat.Minimum);
            Assert.Equal(int.MaxValue, stat.Maximum);
        }
    }
}