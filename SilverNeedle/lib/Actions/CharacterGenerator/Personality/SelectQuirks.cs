// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGenerator.Personality
{
    using System;
    using HandlebarsDotNet;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SelectQuirks : ICharacterDesignStep
    {
        EntityGateway<QuirkTemplate> quirkGateway;
        public SelectQuirks()
        {
            this.quirkGateway= GatewayProvider.Get<QuirkTemplate>();
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var selected = quirkGateway.Choose(strategy.QuirkCount);
            var quirks = new Quirks();
            foreach(var q in selected)
            {
            
                quirks.Items.Add(CreateSentence(character, q));
            }
            character.Add(quirks);
        }

        private string CreateSentence(CharacterSheet character, DescriptionDetail selected)
        {
            SilverNeedle.Utility.HandlebarsHelpers.ConfigureHelpers();
            var template = selected.Templates.ChooseOne();
            var compile = Handlebars.Compile(template);
            var commonProperties = new {
                name = character.Name,
                pronoun = character.Gender.Pronoun(),
                possessivepronoun = character.Gender.PossessivePronoun(),
                description = selected.CreateDescription(),
                feature = selected.Name,
                descriptors = selected.Descriptors
            };
            var sentence = compile.Invoke(commonProperties);
            
            return sentence.Capitalize();
        }
    }
}