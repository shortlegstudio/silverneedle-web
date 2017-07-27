// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Maps;

    [TestFixture]
    public class UpgradeFavoredTerrainTests
    {
        [Fact]
        public void SelectsAFavoredEnemyAndAddsTwoToTheBonus()
        {
            var forest = new TerrainType("Forest");
            var fav = new FavoredTerrain();
            fav.Add(forest);
            var character = new CharacterSheet();
            character.Add(fav);
            var upgrade = new UpgradeFavoredTerrain();
            upgrade.Process(character, new CharacterBuildStrategy());
            Assert.That(fav.Bonus(forest), Is.EqualTo(4));
        }
    }
}