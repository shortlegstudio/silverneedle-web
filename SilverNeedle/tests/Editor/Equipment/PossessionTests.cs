// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;

    [TestFixture]
    public class PossessionTests
    {
        [Test]
        public void PossessionsAreAssociatedWithGearAndImpersonateThem()
        {
            var gear = new Gear("foo", 2, 10);
            var possession = new Possession(gear);
            Assert.That(possession.Name, Is.EqualTo("foo"));
            Assert.That(possession.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void PossessionsContainQuantityForGearThatSupportsIt()
        {
            var gear = new Gear("foo", 2, 10);
            var possession = new Possession(gear);
            possession.Quantity = 10;
            Assert.That(possession.Quantity, Is.EqualTo(10));
        }

        [Test]
        public void YouCanIncrementQuantityToCountSimilarItems()
        {
            var gear = new Gear("foo", 2, 10);
            var possession = new Possession(gear);
            possession.IncrementQuantity();
            Assert.That(possession.Quantity, Is.EqualTo(2));
        }

    }
}