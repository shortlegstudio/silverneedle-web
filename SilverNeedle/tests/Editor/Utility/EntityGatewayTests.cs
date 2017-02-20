// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle.Utility;
using System.Linq;

namespace Utility {

    [TestFixture]
    public class EntityGatewayTests {
        [Test]
        public void LoadsClassesFromObjectStores() {
            var data = new List<IObjectStore>();
            var entity1 = new MemoryStore();
            entity1.SetValue("name", "prop1");
            var entity2 = new MemoryStore();
            entity2.SetValue("name", "prop2");
            data.Add(entity1);
            data.Add(entity2);

            var g = new EntityGateway<TestObject>(data);
            var all = g.All();
            Assert.AreEqual(1, g.Where(x => x.Name == "prop1").Count());
            Assert.AreEqual(1, g.Where(x => x.Name == "prop2").Count());
        }

        public class TestObject {
            public string Name { get; private set; }
            public TestObject(IObjectStore data) {
                Name = data.GetString("name");
            }
        }
    }
}