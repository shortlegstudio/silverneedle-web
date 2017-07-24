// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class IdealTests 
    {
        [Test]
        public void ParseFromObjectStore()
        {
            var store = new MemoryStore();
            store.SetValue("name", "Faith");
            store.SetValue("description", "Trust in my deity.");

            var ideal = new Ideal(store);
            Assert.AreEqual("Faith", ideal.Name);
            Assert.AreEqual("Trust in my deity.", ideal.Description);
        }
    }
}