// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using HandlebarsDotNet;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Lexicon;

    public class SelectCharacterFavoriteColorsTests
    {

        [Fact]
        public void PrintsOutColorNameFromOneOfCharactersFavoriteColors()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var likes = bob.Get<Likes>();
            var color = new Color("Red", 255, 0, 0);
            likes.SetFavoriteColors(new Color[] { color });

            SelectCharacterFavoriteColors.CreateAndRegister();
            var handleBarsTemplate = Handlebars.Compile("{{choose-favorite-color}}");
            var result = handleBarsTemplate(new CharacterContext(bob).CreateObject());
            Assert.Equal("Red", result);
        }

        [Fact]
        public void IfCharacterDoesNotHaveFavoriteColorsJustUseBlack()
        {
            var bob = CharacterTestTemplates.AverageBob();
            SelectCharacterFavoriteColors.CreateAndRegister();
            var handleBarsTemplate = Handlebars.Compile("{{choose-favorite-color}}");
            var result = handleBarsTemplate(new CharacterContext(bob).CreateObject());
            Assert.Equal("black", result);
        }
    }    

    


}