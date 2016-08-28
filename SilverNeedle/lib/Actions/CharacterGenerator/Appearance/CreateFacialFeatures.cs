// //-----------------------------------------------------------------------
// // <copyright file="CreateFacialFeatures.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using SilverNeedle.Characters.Appearance;

namespace SilverNeedle.Actions.CharacterGenerator.Appearance
{
    using SilverNeedle;

    public class CreateFacialFeatures
    {
        public CreateFacialFeatures()
        {
        }

        public FacialDescription CreateFace(Gender gender)
        {
            var facial = new FacialDescription();
            facial.EyeColor = EnumHelpers.ChooseOne<EyeColors>();
            facial.HairColor = EnumHelpers.ChooseOne<HairColors>();
            facial.HairStyle = EnumHelpers.ChooseOne<HairStyles>();

            if (gender == Gender.Male)
            {
                facial.FacialHair = EnumHelpers.ChooseOne<FacialHairStyles>();
            }
            else
            {
                facial.FacialHair = FacialHairStyles.None;
            }

            return facial;
        }

    }
}

