// //-----------------------------------------------------------------------
// // <copyright file="FacialDescriptionTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using NUnit.Framework;
using SilverNeedle.Characters.Appearance;


namespace Characters
{
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

