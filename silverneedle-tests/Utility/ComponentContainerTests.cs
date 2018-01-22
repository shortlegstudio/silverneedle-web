// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Utility
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Utility;
    
    public class ComponentContainerTests
    {
        [Fact]
        public void AddingNullComponentResultsInExceptionBeingThrown()
        {
            var contain = new ComponentContainer();
            Assert.Throws(
                typeof(System.ArgumentNullException),
                () => contain.Add(null)
            );
        }

        [Fact]
        public void StatModifiersShouldApplyToAllMatchingStatistics()
        {
            var contain = new ComponentContainer();
            var stat1 = new BasicStat("A Statistic");
            var stat2 = new BasicStat("A Statistic");
            contain.Add(stat1);
            contain.Add(stat2);

            var mod = new ValueStatModifier("A Statistic", 10, "foo");
            contain.Add(mod);
            Assert.Equal(10, stat1.TotalValue);
            Assert.Equal(10, stat2.TotalValue);
        }

        [Fact]
        public void IfStatModifierHasStatisticTypeSetDoNotApplyToUnmatchingTypes()
        {
            var contain = new ComponentContainer();
            var custom = new CustomStatType("Stat");
            var stat = new BasicStat("Stat 2");
            var mod = new ValueStatModifier("%stat%", 10, "foo");
            mod.StatisticType = "Tests.Utility.CustomStatType";

            contain.Add(custom);
            contain.Add(stat);
            contain.Add(mod);
            Assert.Equal(10, custom.TotalValue);
            Assert.Equal(0, stat.TotalValue);
        }

        [Fact(Skip="Cannot implement this until weapon modifiers are managed")]
        public void ThrowsExceptionIfCannotApplyModifierBecauseStatIsNotFound()
        {
            var container = new ComponentContainer();
            var mod = new ValueStatModifier("Stat", 10, "Foo");
            Assert.Throws(typeof(StatisticNotFoundException), () => container.Add(mod));
        }

    }

    public class CustomStatType : BasicStat
    {
        public CustomStatType(string name) : base(name) { }
    }
}