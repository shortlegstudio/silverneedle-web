// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Appearance
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Utility;

    [TestFixture]
    public class DescriptionDetailTests
    {
        [Test]
        public void DescriptionDetailCanHaveSupportingAdjectivesToChooseFrom()
        {
            var data = new MemoryStore();
            data.SetValue("name", "ponytail");
            var descriptors = new MemoryStore();
            descriptors.AddListItem(new MemoryStore("descriptor", "long, short, waist-length"));
            descriptors.AddListItem(new MemoryStore("descriptor", "messy, tidy, wavy, straight, high, braided"));
            data.SetValue("descriptors", descriptors);

            var subject = new TestDescription(data);
            Assert.That(subject.Name, Is.EqualTo("ponytail"));
            Assert.That(subject.Descriptors.ElementAt(0), Has.Exactly(1).EqualTo("long"));
            Assert.That(subject.Descriptors.ElementAt(1), Has.Exactly(1).EqualTo("wavy"));
        }

        [Test]
        public void CreatingADescriptionCombinesDescriptorsAndName()
        {
            var data = new MemoryStore();
            data.SetValue("name", "ponytail");
            var descriptors = new MemoryStore();
            descriptors.AddListItem(new MemoryStore("descriptor", "long"));
            descriptors.AddListItem(new MemoryStore("descriptor", "braided"));
            data.SetValue("descriptors", descriptors);
            var subject = new TestDescription(data); 
            var text = subject.CreateDescription();
            Assert.That(text, Is.EqualTo("long, braided ponytail"));
        }

        private class TestDescription : DescriptionDetail
        {
            public TestDescription(IObjectStore data) : base(data)
            {

            }
        }
    }
}