// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Characters.Appearance;

    [TestFixture]
    public class FacialDescriptionTests
    {
        [Test]
        public void FacialDescriptionsHaveEyeAndHairColors()
        {
            var facial = new FacialDescription();
            facial.EyeColor = new EyeColor("Amber");
            facial.HairColor = new HairColor("Black");
            facial.HairStyle = new HairStyle("Buzzcut");
            facial.FacialHair = new FacialHair("Moustache");

            Assert.That(facial.FacialHair.Name, Is.EqualTo("Moustache"));
        }

        [Test]
        public void EmptyConstructorInitializesSubclass()
        {
            var facial = new FacialDescription();
            Assert.That(facial.HairStyle, Is.Not.Null);
            Assert.That(facial.HairColor, Is.Not.Null);
            Assert.That(facial.FacialHair, Is.Not.Null);
            Assert.That(facial.EyeColor, Is.Not.Null);
        }
    }
}

