// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Describe
{
    using HandlebarsDotNet;
    using SilverNeedle.Characters;
    public class DescriptionBuilder
    {

        public string FormatString { get; set; }
        public string Describe(CharacterSheet character)
        {
            var compile = Handlebars.Compile(FormatString);
            var characterTextView = new CharacterSheetTextView(character);
            return compile(characterTextView).Capitalize();
        }

    }
}