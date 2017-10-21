// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.Personality
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Personality;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class SelectFavoriteColorsTests
    {
        [Fact]
        public void ChoosesAFewColorsThatTheCharacterMightLike()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Strategy.FavoriteColorCount = 3;
            var colors = new Color[]
            {
                new Color("Red", 255, 0, 0),
                new Color("Green", 0, 255, 0),
                new Color("Blue", 0, 0, 255)
            };

            var pick = new SelectFavoriteColors(EntityGateway<Color>.LoadFromList(colors));
            pick.ExecuteStep(bob);

            var personality = bob.Get<Likes>();
            Assert.Contains(colors[0], personality.FavoriteColors);
        }
    }

}