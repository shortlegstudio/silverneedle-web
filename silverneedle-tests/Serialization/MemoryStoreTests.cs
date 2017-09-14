// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Serialization;

    public class MemoryStoreTests
    {
        [Fact]
        public void CanAddValuesToMemoryStore()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("node", "bar");
            Assert.Equal("bar", memStore.GetString("node"));
            Assert.Equal(1, memStore.Keys.Count());
            Assert.Equal("node", memStore.Keys.First());
        }

        [Fact]
        public void CanSetAndGetIntegers()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("foo", 2);
            Assert.Equal(2, memStore.GetInteger("foo"));
        }

        [Fact]
        public void GetStringOptionalReturnsAnEmptyStringIfKeyIsMissing()
        {
            var memStore = new MemoryStore();
            Assert.Equal("", memStore.GetStringOptional("missingKey"));
        }

        [Fact]
        public void CanDetermineWhetherAKeyExists()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("foo", "bar");
            Assert.True(memStore.HasKey("foo"));
            Assert.False(memStore.HasKey("somethingCrazy"));
        }

        [Fact]
        public void CanGetAnEnum()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("enum", "Bar");
            Assert.Equal(TestEnum.Bar, memStore.GetEnum<TestEnum>("enum"));
        }

        [Fact]
        public void CanSetAndGetBools()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("bool", true);
            memStore.SetValue("falsies", false);
            Assert.True(memStore.GetBool("bool"));
            Assert.False(memStore.GetBool("falsies"));
        }

        [Fact]
        public void CanSetStringArraysForLists()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("options", new string[] { "option1", "option2", "option3" });
            Assert.Equal(new string[] { "option1", "option2", "option3" }, memStore.GetList("options"));
        }

        public enum TestEnum
        {
            Foo,
            Bar
        }
    }
}