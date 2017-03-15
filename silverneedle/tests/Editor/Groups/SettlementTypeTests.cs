// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Groups
{
    using NUnit.Framework;
    using SilverNeedle.Groups;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SettlementTypeTests
    {
        [Test]
        public void ParseObjectStore()
        {
            var t = new MemoryStore();
            t.SetValue("name", "Village");
            t.SetValue("minimum", 100);
            t.SetValue("maximum", 500);

            var setType = new SettlementType(t);
            Assert.AreEqual("Village", setType.Name);
            Assert.AreEqual(100, setType.MinimumPopulation);
            Assert.AreEqual(500, setType.MaximumPopulation);

        }
    }

}