// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Personalities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Lexicon;
    public class Likes
    {
        private Color[] favoriteColors = new Color[] { }; 
        public IEnumerable<Color> FavoriteColors
        {
            get { return this.favoriteColors; }
        }

        public void SetFavoriteColors(IEnumerable<Color> colors)
        {
            this.favoriteColors = colors.ToArray();
        }
    }
}