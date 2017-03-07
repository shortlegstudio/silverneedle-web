// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle.Utility;
using System.Linq;
using SilverNeedle.Characters;
using System;

namespace Utility {

    [TestFixture]
    public class EntityGatewayTests {
        private EntityGateway<TestObject> subject;

        [SetUp]
        public void SetUp()
        {
            var data = new List<IObjectStore>();
            var entity1 = new MemoryStore();
            entity1.SetValue("name", "prop1");
            var entity2 = new MemoryStore();
            entity2.SetValue("name", "prop2");
            data.Add(entity1);
            data.Add(entity2);

            subject = new EntityGateway<TestObject>(data);
        }

        [Test]
        public void LoadsClassesFromObjectStores() {           
            Assert.AreEqual(1, subject.Where(x => x.Name == "prop1").Count());
            Assert.AreEqual(1, subject.Where(x => x.Name == "prop2").Count());
        }

        [Test]
        public void LoadsDirectlyFromDatafileLoaderByDefault()
        {
            var gateway = new EntityGateway<PersonalityType>();
            Assert.Greater(gateway.All().Count(), 0);
        }

        [Test]
        public void CanReturnASingleEntityThatMatches()
        {
            var prop1 = subject.Find("prop1");
            Assert.IsNotNull(prop1);
        }

        public class TestObject : IGatewayObject {
            public string Name { get; private set; }
            public TestObject(IObjectStore data) {
                Name = data.GetString("name");
            }

            public bool Matches(string name)
            {
                return Name == name;
            }
        }
    }
}