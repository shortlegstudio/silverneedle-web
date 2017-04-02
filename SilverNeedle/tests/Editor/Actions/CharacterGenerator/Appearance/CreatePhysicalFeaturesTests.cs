// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.Appearance
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Appearance;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class CreatePhysicalFeaturesTests
    {
        [Test]
        public void IfNoTemplateOrLocationJustDoesASimpleFormat()
        {
            var mem = new MemoryStore();
            mem.SetValue("name", "crooked nose");
            var phys = new PhysicalFeature(mem);
            var gateway = new EntityGateway<PhysicalFeature>(new PhysicalFeature[] { phys });

            var subject = new CreatePhysicalFeatures(gateway);
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Appearance.PhysicalAppearance, Is.EqualTo("He has a crooked nose."));

        }

        [Test]
        public void LocationsAreSelectedToMakeItMoreInteresting()
        {
            var mem = new MemoryStore();
            mem.SetValue("name", "jagged scar");
            mem.SetValue("locations", "left arm");
            var temps = new MemoryStore();
            temps.AddListItem(new MemoryStore("template", "{{pronoun}} has a {{feature}} on {{possessivepronoun}} {{location}}."));
            mem.SetValue("templates", temps);
            var phys = new PhysicalFeature(mem);
            var gateway = new EntityGateway<PhysicalFeature>(new PhysicalFeature[] { phys });

            var subject = new CreatePhysicalFeatures(gateway);
            var character = new CharacterSheet();
            character.Gender = Gender.Female;
            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Appearance.PhysicalAppearance, Is.EqualTo("She has a jagged scar on her left arm."));
        }

        [Test]
        public void UsesDescriptorsIfAvailable()
        {
            var descs = new MemoryStore();
            descs.AddListItem(new MemoryStore("descriptor", "dragon"));
            var mem = new MemoryStore();
            mem.SetValue("name", "tattoo");
            mem.SetValue("descriptors", descs);
            var phys = new PhysicalFeature(mem);

            var gateway = new EntityGateway<PhysicalFeature>(new PhysicalFeature[] { phys });
            var subject = new CreatePhysicalFeatures(gateway);

            var character = new CharacterSheet();
            character.Gender = Gender.Female;

            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Appearance.PhysicalAppearance, Is.EqualTo("She has a dragon tattoo."));
        }

        [Test]
        public void BreaksDescriptionIntoIndividualComponentsForMoreFlexibleDescriptions()
        {
            // Set up Descriptors
            var descs = new MemoryStore();
            descs.AddListItem(new MemoryStore("pattern", "dragon"));
            descs.AddListItem(new MemoryStore("color", "black"));

            // Set up Templates 
            var temps = new MemoryStore();
            temps.AddListItem(new MemoryStore("template", "{{feature}} of a {{descriptor \"color\"}} {{descriptor \"pattern\"}}."));

            // Set up physical Feature
            var mem = new MemoryStore();
            mem.SetValue("name", "tattoo");
            mem.SetValue("descriptors", descs);
            mem.SetValue("templates", temps);
            var phys = new PhysicalFeature(mem);

            var gateway = new EntityGateway<PhysicalFeature>(new PhysicalFeature[] { phys });
            var subject = new CreatePhysicalFeatures(gateway);

            var character = new CharacterSheet();
            character.Gender = Gender.Female;

            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Appearance.PhysicalAppearance, Is.EqualTo("Tattoo of a black dragon."));
        }
    }
}