// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Maps;

    
    public class FavoredTerrainTests
    {
        [Fact]
        public void NameShouldReflectFavoredTerrainAndBonus()
        {
            var terrain = new TerrainType("Jungle");
            var fav = new FavoredTerrain();
            fav.Add(terrain);
            Assert.Equal(fav.DisplayString(), "Favored Terrain (Jungle +2)");
        }

        [Fact]
        public void CanHaveMultipleFavoredEnemies()
        {
            var forest = new TerrainType("Forest");
            var plains = new TerrainType("Plains");

            var fav = new FavoredTerrain();
            fav.Add(forest);
            fav.Add(plains);
            Assert.Contains(forest, fav.TerrainTypes);
            Assert.Contains(plains, fav.TerrainTypes);
            Assert.Equal(fav.Bonus(forest), 2);
            Assert.Equal(fav.Bonus(plains), 2);
            Assert.Equal(fav.DisplayString(), "Favored Terrain (Forest +2, Plains +2)");
        }
    }
}