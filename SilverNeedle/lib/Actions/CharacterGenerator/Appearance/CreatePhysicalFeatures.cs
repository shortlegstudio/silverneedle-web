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
    using SilverNeedle.General;
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
            ConfigureHelpers();
            var selected = physical.ChooseOne();
            var template = selected.Templates.ChooseOne();
            var location = selected.Locations.ChooseOne();
            var compile = Handlebars.Compile(template);
            var commonProperties = new {
                name = character.Name,
                pronoun = character.Gender.Pronoun(),
                possessivepronoun = character.Gender.PossessivePronoun(),
                description = selected.CreateDescription(),
                location = location,
                feature = selected.Name,
                descriptors = selected.Descriptors
            };
            var sentence = compile.Invoke(commonProperties);
            
            character.Appearance.PhysicalAppearance = sentence.Capitalize();
        }

        private void ConfigureHelpers() {
            Handlebars.RegisterHelper("descriptor", (writer, context, parameters) => {
                var value = context.descriptors[parameters[0].ToString()] as string[];
                writer.Write(value.ChooseOne());
            });

            Handlebars.RegisterHelper("choose-color", (writer, context, parameters) => {
                var color = GatewayProvider.Get<Color>().ChooseOne();
                writer.Write(color.Name);
            });
        }
    }
}

