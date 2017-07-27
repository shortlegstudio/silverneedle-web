// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Appearance
{
    using NUnit.Framework;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class PhysicalFeatureTests
    {
        [Fact]
        public void HasADefaultTemplate()
        {
            var store = new MemoryStore();
            store.SetValue("name", "tattoo");
            var feature = new PhysicalFeature(store);
            Assert.That(feature.Templates, Has.Exactly(1).EqualTo("{{pronoun}} has a {{description}}."));
        }
    }
}