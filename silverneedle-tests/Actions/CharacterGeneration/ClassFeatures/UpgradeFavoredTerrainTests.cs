// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Maps;

    
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
            upgrade.ExecuteStep(character, new CharacterStrategy());
            Assert.Equal(fav.Bonus(forest), 4);
        }
    }
}