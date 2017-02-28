
using System;
using NUnit.Framework;
using System.Linq;
using SilverNeedle.Utility;

namespace Utility
{
    [TestFixture]
    public class MemoryStoreTests
    {
        [Test]
        public void CanAddValuesToMemoryStore()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("node", "bar");
            Assert.AreEqual("bar", memStore.GetString("node"));
            Assert.AreEqual(1, memStore.Keys.Count());
            Assert.AreEqual("node", memStore.Keys.First());
        }

        [Test]
        public void CanSetAndGetIntegers()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("foo", 2);
            Assert.AreEqual(2, memStore.GetInteger("foo"));
        }

        [Test]
        public void GetStringOptionalReturnsAnEmptyStringIfKeyIsMissing()
        {
            var memStore = new MemoryStore();
            Assert.AreEqual("", memStore.GetStringOptional("missingKey"));
        }

        [Test]
        public void CanDetermineWhetherAKeyExists()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("foo", "bar");
            Assert.IsTrue(memStore.HasKey("foo"));
            Assert.IsFalse(memStore.HasKey("somethingCrazy"));
        }

        [Test]
        public void CanGetAnEnum()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("enum", "Bar");
            Assert.AreEqual(TestEnum.Bar, memStore.GetEnum<TestEnum>("enum"));
        }

        [Test]
        public void CanSetAndGetBools()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("bool", true);
            memStore.SetValue("falsies", false);
            Assert.IsTrue(memStore.GetBool("bool"));
            Assert.IsFalse(memStore.GetBool("falsies"));
        }

        public enum TestEnum
        {
            Foo,
            Bar
        }
    }
}