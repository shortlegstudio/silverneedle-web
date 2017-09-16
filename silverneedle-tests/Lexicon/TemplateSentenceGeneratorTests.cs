// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class TemplateSentenceGeneratorTests
    {
        [Fact]
        public void DescriptionDetailCanHaveSupportingAdjectivesToChooseFrom()
        {
            var data = new MemoryStore();
            data.SetValue("name", "ponytail");
            var descriptors = new MemoryStore();
            descriptors.AddListItem(new MemoryStore("length", new string[] { "long", "short", "waist-length" }));
            descriptors.AddListItem(new MemoryStore("texture", new string[] { "messy", "tidy", "wavy", "straight", "high", "braided" }));
            data.SetValue("descriptors", descriptors);

            var subject = new TestDescription(data);
            Assert.Equal(subject.Name, "ponytail");
            Assert.Single(subject.Descriptors.Values.ElementAt(0), "long");
            Assert.Single(subject.Descriptors.Values.ElementAt(1), "wavy");
        }

        [Fact]
        public void CreatingADescriptionCombinesDescriptorsAndName()
        {
            var data = new MemoryStore();
            data.SetValue("name", "ponytail");
            var descriptors = new MemoryStore();
            descriptors.AddListItem(new MemoryStore("length", new string[] { "long" }));
            descriptors.AddListItem(new MemoryStore("texture", new string[] { "braided" }));
            data.SetValue("descriptors", descriptors);
            var subject = new TestDescription(data); 
            var text = subject.CreateDescription();
            Assert.Equal(text, "long braided ponytail");
        }

        [Fact]
        public void SupportsNamedDescriptors()
        {
            var data = new MemoryStore();
            data.SetValue("name", "ponytail");
            var descriptors = new MemoryStore();
            descriptors.AddListItem(new MemoryStore("color", "red, green, blue"));
            data.SetValue("descriptors", descriptors);
            var subject = new TestDescription(data);
            Assert.Single(subject.Descriptors.Keys, "color");
        }

        [Fact]
        public void CanAddDescriptors()
        {
            var test = new TestDescription();
            test.AddDescriptor("color", new string[] { "red", "green", "blue"});
            Assert.Equal(new string[] { "red", "green", "blue" }, test.Descriptors["color"]);
        }

        [Fact]
        public void CanAddTemplates()
        {
            var test = new TestDescription();
            test.AddTemplate("Foo {{bar}}");
            Assert.Equal("Foo {{bar}}", test.Templates[0].Template);
        }


        private class TestDescription : TemplateSentenceGenerator
        {
            public TestDescription() { }
            public TestDescription(IObjectStore data) : base(data)
            {

            }
        }
    }
}