// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.Appearance
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Utility;

    public class CreateHair : ICharacterDesignStep
    {
        private EntityGateway<HairStyle> hairStyles;
        private EntityGateway<HairColor> hairColors;
        public CreateHair()
        {
            hairStyles = GatewayProvider.Get<HairStyle>();
            hairColors = GatewayProvider.Get<HairColor>();
        }

        public CreateHair(EntityGateway<HairColor> colors, EntityGateway<HairStyle> styles)
        {
            hairColors = colors;
            hairStyles = styles;
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Appearance.HairColor = hairColors.ChooseOne();
            character.Appearance.HairStyle = hairStyles.ChooseOne();
            character.Appearance.Hair = string.Format("{0} {1}", character.Appearance.HairColor.CreateDescription(), character.Appearance.HairStyle.CreateDescription());
        }
    }
}

