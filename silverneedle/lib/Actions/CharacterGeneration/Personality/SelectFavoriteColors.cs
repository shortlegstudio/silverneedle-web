// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.Personality
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class SelectFavoriteColors : ICharacterDesignStep
    {
        private EntityGateway<Color> colors;

        public SelectFavoriteColors(EntityGateway<Color> colors)
        {
            this.colors = colors;
        }

        public SelectFavoriteColors()
        {
            this.colors = GatewayProvider.Get<Color>();
        }
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var choices = this.colors.Choose(strategy.FavoriteColorCount);
            character.Get<Likes>().SetFavoriteColors(choices);
        }
    }
}