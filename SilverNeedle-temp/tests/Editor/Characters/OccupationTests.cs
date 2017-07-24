// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class OccupationTests
    {
        [Test]
        public void CanParseFromObjectStore()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Pig Farmer");
            data.SetValue("class", "commoner");

            var oc = new Occupation(data);
            Assert.AreEqual("Pig Farmer", oc.Name);
            Assert.AreEqual("commoner", oc.Class);
            
        }
    }
}