// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;

    public class DamageReductionTests
    {
        [Fact]
        public void DamageReductionCanHaveATypeThatBypassTheDefense()
        {
            var dr = new DamageReduction("cold iron", 5);
            Assert.Equal(5, dr.TotalValue);
            Assert.Equal("cold iron", dr.BypassType);
            Assert.Equal("5/cold iron", dr.DisplayString());
        }

        [Fact]
        public void SometimesNoTypeWillBypassDefense()
        {
            var dr = new DamageReduction(1);
            Assert.Equal("1/-", dr.DisplayString());
        }

    }
}