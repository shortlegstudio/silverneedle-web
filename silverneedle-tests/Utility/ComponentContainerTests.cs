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

            var mod = new ValueStatModifier("A Statistic", 10, "foo", "bar");
            contain.Add(mod);
            Assert.Equal(10, stat1.TotalValue);
            Assert.Equal(10, stat2.TotalValue);
        }
    }
}