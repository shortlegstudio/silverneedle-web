// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using Xunit;
    using SilverNeedle.Serialization;

    public class ObjectStoreSerializerTests
    {
        [Fact]
        public void InstantiatesBasicAttributesOnModel()
        {
            var store = new MemoryStore();
            store.SetValue("name", "Foo");
            store.SetValue("number", 5);
            store.SetValue("float", 37.5f);
            store.SetValue("list", "one, two, three, four");
            store.SetValue("dice", "1d6+4");
            var obj = new TestSimpleObject();
            var test = store.Deserialize<TestSimpleObject>(obj);
            Assert.Equal(test.Name, "Foo");
            Assert.Equal(test.Number, 5);
            Assert.Equal(test.FloatNumber, 37.5f);
            Assert.NotStrictEqual(test.ListOfValues, new string[] { "one", "two", "three", "four" });
            Assert.Equal(test.Optional, "");
            Assert.Equal("defaultString", test.OptionalWithDefault);
            Assert.Equal("1d6+4", test.DiceValues.ToString());
        }

        //TODO: Question? If the property is invalid what should be the expected behavior?

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

            [ObjectStoreOptional("optional")]
            public string Optional { get; set; }
            
            [ObjectStoreOptional("optional-default", "defaultString")]
            public string OptionalWithDefault { get; set; }

            [ObjectStore("dice")]
            public SilverNeedle.Dice.Cup DiceValues { get; private set; }

            public string IgnoreMe { get; set; }

            public bool Matches(string name)
            {
                return false;
            }
        }
    }

}