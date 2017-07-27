// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment
{
    using Xunit;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class GearTests
    {
        [Fact]
        public void GearLoadsBasicAttributesForTheItem()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Backpack");
            data.SetValue("weight", 39.2f);
            data.SetValue("value", "23gp");

            var backpack = new Gear(data);
            Assert.Equal(backpack.Name, "Backpack");
            Assert.Equal(backpack.Weight, 39.2f);
            Assert.Equal(backpack.Value, 2300);
            Assert.True(backpack.GroupSimilar);
        }
    }
}