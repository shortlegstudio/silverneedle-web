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
        [Test]
        public void PhysicalFeaturesCanHaveLocationsAndTemplatesToDescribeThem()
        {
            var store = new MemoryStore();
            store.SetValue("name", "tattoo");
            store.SetValue("locations", "left arm, right arm");
            var templates = new MemoryStore();
            templates.AddListItem(new MemoryStore("template", "{{pronoun}} template."));
            store.SetValue("templates", templates);
            var feature = new PhysicalFeature(store);

            Assert.That(feature.Locations, Has.Exactly(1).EqualTo("left arm"));
            Assert.That(feature.Locations, Has.Exactly(1).EqualTo("right arm"));
            Assert.That(feature.Templates, Has.Exactly(1).EqualTo("{{pronoun}} template."));
        }

        [Test]
        public void HasADefaultTemplate()
        {
            var store = new MemoryStore();
            store.SetValue("name", "tattoo");
            var feature = new PhysicalFeature(store);
            Assert.That(feature.Templates, Has.Exactly(1).EqualTo("{{pronoun}} has a {{description}}."));
        }

        [Test]
        public void IfNoLocationSpecifiedJustUseEmptyString()
        {
            var store = new MemoryStore();
            store.SetValue("name", "foo");
            var feature = new PhysicalFeature(store);
            Assert.That(feature.Locations, Has.Exactly(1).EqualTo(""));
        }

        [Test]
        public void IfLocationSpecifiedUseAMoreAdvancedDefaultTemplate()
        {
            var store = new MemoryStore();
            store.SetValue("name", "tattoo");
            store.SetValue("locations", "left arm, right arm");
            var feature = new PhysicalFeature(store);
            Assert.That(feature.Templates, Has.Exactly(1).EqualTo("{{pronoun}} has a {{description}} on {{possessivepronoun}} {{location}}."));
        }
    }
}