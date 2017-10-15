// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Lexicon
{
    using System.IO;
    using HandlebarsDotNet;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    public class ChooseCharacterFavoriteColor : ITemplateExpander
    {

        public static ChooseCharacterFavoriteColor CreateAndRegister()
        {
            return new ChooseCharacterFavoriteColor();
        }
        private ChooseCharacterFavoriteColor()
        {
            RegisterHelper();
        }
        public void ExpandTemplate(TextWriter writer, dynamic context, object[] parameters)
        {
            var character = context.charactersheet as CharacterSheet;
            var likes = character.Get<Likes>();
            var color = likes.FavoriteColors.ChooseOne();
            writer.Write(color.Name);
        }

        private void RegisterHelper()
        {
            HandlebarsDotNet.Handlebars.RegisterHelper("choose-favorite-color", this.ExpandTemplate);
        }
    }
}