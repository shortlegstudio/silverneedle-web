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
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Utility;

    public class CreateFacialFeatures : ICharacterDesignStep
    {
        private EntityGateway<HairStyle> hairStyles;
        private EntityGateway<HairColor> hairColors;
        private EntityGateway<FacialHair> facialHair;
        private EntityGateway<EyeColor> eyeColors;
        public CreateFacialFeatures()
        {
            hairStyles = GatewayProvider.Get<HairStyle>();
            hairColors = GatewayProvider.Get<HairColor>();
            facialHair = GatewayProvider.Get<FacialHair>();
            eyeColors = GatewayProvider.Get<EyeColor>();
        }
        public CharacterAppearance CreateFace(Gender gender)
        {
            var facial = new CharacterAppearance();
            facial.EyeColor = eyeColors.ChooseOne();
            facial.HairColor = hairColors.ChooseOne();
            facial.HairStyle = hairStyles.ChooseOne();

            if (gender == Gender.Male)
            {
                facial.FacialHair = facialHair.ChooseOne();
            }

            return facial;
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Appearance = CreateFace(character.Gender);
        }
    }
}

