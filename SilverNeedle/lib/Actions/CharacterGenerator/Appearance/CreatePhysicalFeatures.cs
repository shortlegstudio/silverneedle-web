// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.Appearance
{
    using HandlebarsDotNet;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Utility;

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
            var template = selected.Templates.ChooseOne();
            var location = selected.Locations.ChooseOne();
            var compile = Handlebars.Compile(template);
            var commonProperties = new {
                name = character.Name,
                pronoun = character.Gender.Pronoun(),
                possessivepronoun = character.Gender.PossessivePronoun(),
                feature = selected.CreateDescription(),
                location = location
            };
            var sentence = compile.Invoke(commonProperties);
            
            character.Appearance.PhysicalAppearance = sentence.Capitalize();
        }
    }
}

