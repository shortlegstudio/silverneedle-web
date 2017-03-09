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
            facial.EyeColor = EyeColors.Amber;
            facial.HairColor = HairColors.Black;
            facial.HairStyle = HairStyles.Crewcut;
            facial.FacialHair = FacialHairStyles.Moustache;

            Assert.AreEqual(FacialHairStyles.Moustache, facial.FacialHair);
        }
    }
}

