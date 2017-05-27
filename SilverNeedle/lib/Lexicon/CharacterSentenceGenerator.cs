// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using HandlebarsDotNet;
    using SilverNeedle.Characters;
    public static class CharacterSentenceGenerator
    {
        public static string Create(CharacterSheet character, DescriptionDetail outline)
        {
            SilverNeedle.Utility.HandlebarsHelpers.ConfigureHelpers();
            var template = outline.Templates.ChooseOne();
            var compile = Handlebars.Compile(template);
            var commonProperties = new {
                name = character.Name,
                pronoun = character.Gender.Pronoun(),
                possessivepronoun = character.Gender.PossessivePronoun(),
                description = outline.CreateDescription(),
                feature = outline.Name,
                descriptors = outline.Descriptors
            };
            var sentence = compile.Invoke(commonProperties);
            
            return sentence.Capitalize();
        }
    }
}