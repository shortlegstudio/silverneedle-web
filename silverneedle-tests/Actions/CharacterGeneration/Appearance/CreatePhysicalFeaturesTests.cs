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

    
    public class CreatePhysicalFeaturesTests : RequiresDataFiles
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
            descs.AddListItem(new MemoryStore("descriptor", new string[] { "dragon" }));
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
            descs.AddListItem(new MemoryStore("pattern", new string[] { "dragon" }));
            descs.AddListItem(new MemoryStore("color", new string[] { "black" }));

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

        [Fact]
        public void CombineMultipleDescriptionsTogetherButDoNotRepeat()
        {
            var tattoo = new PhysicalFeature();
            tattoo.AddDescriptor("color", new string[] { "green" });
            tattoo.AddTemplate("Tattoo of a {{descriptor \"color\"}} dragon.");
            var scar = new PhysicalFeature();
            scar.AddDescriptor("location", new string[] { "face" });
            scar.AddTemplate("A scar on {{descriptor \"location\"}}.");

            var gateway = EntityGateway<PhysicalFeature>.LoadFromList(new PhysicalFeature[] { tattoo, scar });
            var subject = new CreatePhysicalFeatures(gateway);

            var character = new CharacterSheet();
            subject.ExecuteStep(character, new CharacterBuildStrategy());
            Assert.Contains("Tattoo of a green dragon.", character.Appearance.PhysicalAppearance);
            Assert.Contains("A scar on face.", character.Appearance.PhysicalAppearance);
        }
    }
}