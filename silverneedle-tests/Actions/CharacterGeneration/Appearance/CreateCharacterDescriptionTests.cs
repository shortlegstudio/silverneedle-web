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

    public class CreateCharacterDescriptionTests : RequiresDataFiles
    {
        [Fact]
        public void CombinesPhysicalFeaturesAndHairIntoSingleDescription()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Appearance.HairColor = new HairColor("red");
            bob.Appearance.HairStyle = new HairStyle("ponytail");

            var description = new CreateCharacterDescription();
            description.ExecuteStep(bob);

            Assert.Contains("his hair is a red ponytail.", bob.Appearance.Description);

        }

        [Fact]
        public void IncludePhysicalFeaturesIntoTheDescription()
        {

            var bob = CharacterTestTemplates.AverageBob();
            bob.Appearance.PhysicalAppearance = "Tattoos and Scars";
            var description = new CreateCharacterDescription();
            description.ExecuteStep(bob);

            Assert.Contains("Tattoos and Scars", bob.Appearance.Description);
        }

        [Fact]
        public void IncludeEyesIntoTheDescription()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Appearance.EyeColor = new EyeColor("blue");
            var description = new CreateCharacterDescription();
            description.ExecuteStep(bob);

            Assert.Contains("his eyes are blue.", bob.Appearance.Description);
        }
    }

}