// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.Appearance
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Appearance;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Serialization;

    
    public class CreatePhysicalFeaturesTests
    {
        [Fact]
        public void IfNoTemplateOrLocationJustDoesASimpleFormat()
        {
            var mem = new MemoryStore();
            mem.SetValue("name", "crooked nose");
            var phys = new PhysicalFeature(mem);
            var gateway = EntityGateway<PhysicalFeature>.LoadWithSingleItem(phys);

            var subject = new CreatePhysicalFeatures(gateway);
            var character = new CharacterSheet();
            subject.ExecuteStep(character, new CharacterBuildStrategy());

            Assert.Equal(character.Appearance.PhysicalAppearance, "He has a crooked nose.");

        }

        [Fact]
        public void UsesDescriptorsIfAvailable()
        {
            var descs = new MemoryStore();
            descs.AddListItem(new MemoryStore("descriptor", "dragon"));
            var mem = new MemoryStore();
            mem.SetValue("name", "tattoo");
            mem.SetValue("descriptors", descs);
            var phys = new PhysicalFeature(mem);

            var gateway = EntityGateway<PhysicalFeature>.LoadWithSingleItem(phys);
            var subject = new CreatePhysicalFeatures(gateway);

            var character = new CharacterSheet();
            character.Gender = Gender.Female;

            subject.ExecuteStep(character, new CharacterBuildStrategy());

            Assert.Equal(character.Appearance.PhysicalAppearance, "She has a dragon tattoo.");
        }

        [Fact]
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

            var gateway = EntityGateway<PhysicalFeature>.LoadWithSingleItem(phys);
            var subject = new CreatePhysicalFeatures(gateway);

            var character = new CharacterSheet();
            character.Gender = Gender.Female;

            subject.ExecuteStep(character, new CharacterBuildStrategy());

            Assert.Equal(character.Appearance.PhysicalAppearance, "Tattoo of a black dragon.");
        }
    }
}