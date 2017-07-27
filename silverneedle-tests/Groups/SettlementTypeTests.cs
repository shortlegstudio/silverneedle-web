// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Groups
{
    using Xunit;
    using SilverNeedle.Groups;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SettlementTypeTests
    {
        [Fact]
        public void ParseObjectStore()
        {
            var t = new MemoryStore();
            t.SetValue("name", "Village");
            t.SetValue("minimum", 100);
            t.SetValue("maximum", 500);

            var setType = new SettlementType(t);
            Assert.Equal("Village", setType.Name);
            Assert.Equal(100, setType.MinimumPopulation);
            Assert.Equal(500, setType.MaximumPopulation);
        }
    }
}