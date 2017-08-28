// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using SilverNeedle.Lexicon;

    public class PhraseContextTests
    {
        [Fact]
        public void CanAddAndRetrievePropertiesToContext()
        {
            var context = new PhraseContext();
            context.Add("name", "Bob");
            context.Add("age", 15);
            Assert.Equal("Bob", context.GetValue<string>("name"));
            Assert.Equal(15, context.GetValue<int>("age"));
        }

        [Fact]
        public void CanCreateAnObjectWithPropertiesForValues()
        {
            var context = new PhraseContext();
            context.Add("name", "Bob");
            context.Add("age", 28);
            
            dynamic obj = context.CreateObject();
            Assert.Equal("Bob", obj.name);
            Assert.Equal(28, obj.age);
        }

        [Fact]
        public void CanInitializeLikeADictionary()
        {
            var context = new PhraseContext() 
            {
                { "name", "Bob" },
                { "age", 28 }
            };

            Assert.Equal("Bob", context.GetValue<string>("name"));
            Assert.Equal(28, context.GetValue<int>("age"));
        }
    }
}