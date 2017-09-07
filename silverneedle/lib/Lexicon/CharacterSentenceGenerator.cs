// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using SilverNeedle.Characters;
    public static class CharacterSentenceGenerator
    {
        public static string Create(CharacterSheet character, TemplateSentenceGenerator outline)
        {
            HandlebarsHelpers.InitializeHelpers();
            var template = outline.Templates.ChooseOne();
            var context = new PhraseContext() {
                { "name" , character.Name },
                { "pronoun" , character.Gender.Pronoun() },
                { "possessivepronoun" , character.Gender.PossessivePronoun() },
                { "description" , outline.CreateDescription() },
                { "feature" , outline.Name },
                { "descriptors" , outline.Descriptors }
            };
            var sentence = template.WritePhrase(context);
            
            return sentence.Capitalize();
        }
    }
}