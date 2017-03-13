// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    [TestFixture]
    public class GearTests
    {
        [Test]
        public void GearLoadsBasicAttributesForTheItem()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Backpack");
            data.SetValue("weight", 39.2f);
            data.SetValue("value", "23gp");

            var backpack = new Gear(data);
            Assert.That(backpack.Name, Is.EqualTo("Backpack"));
            Assert.That(backpack.Weight, Is.EqualTo(39.2f));
            Assert.That(backpack.Value, Is.EqualTo(2300));
        }
    }
}