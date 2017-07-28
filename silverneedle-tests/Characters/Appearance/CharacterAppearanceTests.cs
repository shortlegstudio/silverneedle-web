// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters.Appearance;

    
    public class CharacterAppearanceTests
    {
        [Fact]
        public void FacialDescriptionsHaveEyeAndHairColors()
        {
            var facial = new CharacterAppearance();
            facial.EyeColor = new EyeColor("Amber");
            facial.HairColor = new HairColor("Black");
            facial.HairStyle = new HairStyle("Buzzcut");
            facial.FacialHair = new FacialHair("Moustache");

            Assert.Equal(facial.FacialHair.Name, "Moustache");
        }

        [Fact]
        public void EmptyConstructorInitializesSubclass()
        {
            var facial = new CharacterAppearance();
            Assert.NotNull(facial.HairStyle);
            Assert.NotNull(facial.HairColor);
            Assert.NotNull(facial.FacialHair);
            Assert.NotNull(facial.EyeColor);
        }
    }
}

