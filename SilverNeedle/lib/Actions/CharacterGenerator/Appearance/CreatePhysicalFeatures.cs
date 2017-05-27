// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.Appearance
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class CreatePhysicalFeatures : ICharacterDesignStep
    {
        EntityGateway<PhysicalFeature> physical;
        public CreatePhysicalFeatures()
        {
            physical = GatewayProvider.Get<PhysicalFeature>();
        }

        public CreatePhysicalFeatures(EntityGateway<PhysicalFeature> physicalFeatures)
        {
            physical = physicalFeatures;
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var selected = physical.ChooseOne();
            character.Appearance.PhysicalAppearance = CharacterSentenceGenerator.Create(character, selected);
        }

    }
}

