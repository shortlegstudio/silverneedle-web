// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using NUnit.Framework;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class ObjectStoreSerializerTests
    {
        [Test]
        public void InstantiatesBasicAttributesOnModel()
        {
            var store = new MemoryStore();
            store.SetValue("name", "Foo");
            store.SetValue("number", 5);
            store.SetValue("float", 37.5f);
            store.SetValue("list", "one, two, three, four");
            var obj = new TestSimpleObject();
            var test = store.Deserialize<TestSimpleObject>(obj);
            Assert.That(test.Name, Is.EqualTo("Foo"));
            Assert.That(test.Number, Is.EqualTo(5));
            Assert.That(test.FloatNumber, Is.EqualTo(37.5f));
            Assert.That(test.ListOfValues, Is.EquivalentTo(new string[] { "one", "two", "three", "four" }));
            Assert.That(test.Optional, Is.EqualTo(""));
        }

        public class TestSimpleObject : IGatewayObject
        {
            [ObjectStore("name")]
            public string Name { get; set; }

            [ObjectStore("number")]
            public int Number { get; set; }
            [ObjectStore("float")]
            public float FloatNumber { get; set; }

            [ObjectStore("list")]
            public string[] ListOfValues { get; set; }

            [ObjectStore("optional", true)]
            public string Optional { get; set; }

            public string IgnoreMe { get; set; }

            public bool Matches(string name)
            {
                return false;
            }
        }
    }

}