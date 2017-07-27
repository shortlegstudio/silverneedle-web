// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;

    public class PossessionTests
    {
        [Fact]
        public void PossessionsAreAssociatedWithGearAndImpersonateThem()
        {
            var gear = new Gear("foo", 2, 10);
            var possession = new Possession(gear);
            Assert.Equal(possession.Name, "foo");
            Assert.Equal(possession.Quantity, 1);
        }

        [Fact]
        public void PossessionsContainQuantityForGearThatSupportsIt()
        {
            var gear = new Gear("foo", 2, 10);
            var possession = new Possession(gear);
            possession.Quantity = 10;
            Assert.Equal(possession.Quantity, 10);
        }

        [Fact]
        public void YouCanIncrementQuantityToCountSimilarItems()
        {
            var gear = new Gear("foo", 2, 10);
            var possession = new Possession(gear);
            possession.IncrementQuantity();
            Assert.Equal(possession.Quantity, 2);
        }
    }
}