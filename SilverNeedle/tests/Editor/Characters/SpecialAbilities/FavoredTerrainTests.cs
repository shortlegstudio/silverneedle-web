// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Maps;

    [TestFixture]
    public class FavoredTerrainTests
    {
        [Test]
        public void NameShouldReflectFavoredTerrainAndBonus()
        {
            var terrain = new TerrainType("Jungle");
            var fav = new FavoredTerrain();
            fav.Add(terrain);
            Assert.That(fav.Name, Is.EqualTo("Favored Terrain (Jungle +2)"));
        }

        [Test]
        public void CanHaveMultipleFavoredEnemies()
        {
            var forest = new TerrainType("Forest");
            var plains = new TerrainType("Plains");

            var fav = new FavoredTerrain();
            fav.Add(forest);
            fav.Add(plains);
            Assert.That(fav.TerrainTypes, Contains.Item(forest));
            Assert.That(fav.TerrainTypes, Contains.Item(plains));
            Assert.That(fav.Bonus(forest), Is.EqualTo(2));
            Assert.That(fav.Bonus(plains), Is.EqualTo(2));
            Assert.That(fav.Name, Is.EqualTo("Favored Terrain (Forest +2, Plains +2)"));
        }
    }
}