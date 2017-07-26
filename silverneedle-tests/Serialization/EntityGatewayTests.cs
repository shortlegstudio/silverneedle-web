// Copyright (c) 2017 Trevor Redfern
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization 
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Serialization;
    using System.Linq;
    using SilverNeedle.Characters;
    using System;

    public class EntityGatewayTests 
    {
        private EntityGateway<TestObject> subject;

        public EntityGatewayTests()
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

        [Fact]
        public void LoadsClassesFromObjectStores() {           
            Assert.Equal(1, subject.Where(x => x.Name == "prop1").Count());
            Assert.Equal(1, subject.Where(x => x.Name == "prop2").Count());
        }

        [Fact]
        public void LoadsDirectlyFromDatafileLoaderByDefault()
        {
            var gateway = new EntityGateway<PersonalityType>();
            Assert.True(gateway.All().Count() > 0);
        }

        [Fact]
        public void CanReturnASingleEntityThatMatches()
        {
            var prop1 = subject.Find("prop1");
            Assert.NotNull(prop1);
        }

        [Fact]
        public void CanForceGatewayToASpecificList()
        {
            var list = new List<TestObject>();
            list.Add(new TestObject("hello1"));
            list.Add(new TestObject("hello2"));
            var gateway = new EntityGateway<TestObject>(list);
            Assert.Equal(2, gateway.Count());
            Assert.NotNull(gateway.Find("hello2"));
        }

        [Fact]
        public void UsesCustomSerializerIfPossible()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Foo");
            var gateway = new EntityGateway<TestSerialized>(new IObjectStore[] { data });    
            var first = gateway.All().First();
            Assert.Equal(first.Name, "Foo");
        }

        [Fact]
        public void ObjectsFlaggedAsAlwaysFreshShouldReloadEveryRequest()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Foo");
            var data2 = new MemoryStore();
            data2.SetValue("name", "Bar");
            var gateway = new EntityGateway<AlwaysFresh>(new IObjectStore[] { data, data2 });

            var item = gateway.Find("Foo");
            item.SomeValue = false;

            var itemAgain = gateway.Find("Foo");
            Assert.True(itemAgain.SomeValue);
            
        }

        [Fact]
        public void ThrowsNotFoundExceptionIfObjectIsnotFound()
        {
            var gateway = new EntityGateway<TestObject>(new TestObject[] { new TestObject("Bar") });
            Assert.Throws(typeof(EntityNotFoundException), () => {gateway.Find("Foo"); });
        }

        [Fact]
        public void CustomImplementationForObjectIsAllowed()
        {
            SilverNeedle.ShortLog.DebugFormat("Running Tests");
            var data = new MemoryStore();
            data.SetValue("name", "Foo");
            data.SetValue("custom-implementation", "Tests.Serialization.EntityGatewayTests+TestObjectCustom");
            var data2 = new MemoryStore();
            data2.SetValue("name", "Bar");
            var gateway = new EntityGateway<TestObject>(new IObjectStore[] { data, data2});
            var one = gateway.Find("Foo");
            Assert.IsType<TestObjectCustom>(one);
            var two = gateway.Find("Bar");
            Assert.IsType<TestObject>(two);
        }

        [ObjectStoreSerializable]
        public class AlwaysFresh : IGatewayObject, IGatewayCopy<AlwaysFresh>
        {
            [ObjectStore("name")]
            public string Name { get; set; }

            public bool SomeValue = true;
            public bool Matches(string name)
            {
                return Name.Equals(name);
            }

            public AlwaysFresh() { }

            public AlwaysFresh(AlwaysFresh prev)
            {
                this.Name = prev.Name;
                this.SomeValue = prev.SomeValue;
            }

            public AlwaysFresh Copy()
            {
                return new AlwaysFresh(this);
            }
        }

        [ObjectStoreSerializable]
        public class TestSerialized : IGatewayObject
        {
            [ObjectStore("name")]
            public string Name { get; set; }

            public bool Matches(string name)
            {
                return true;
            }
        }

        public class TestObject : IGatewayObject {
            public string Name { get; private set; }
            public TestObject(IObjectStore data) {
                Name = data.GetString("name");
            }

            public TestObject(string name)
            {
                Name = name;
            }

            public bool Matches(string name)
            {
                return Name == name;
            }
        }

        public class TestObjectCustom : TestObject 
        { 
            public TestObjectCustom(IObjectStore data) : base(data)
            {

            }
        }
    }
}