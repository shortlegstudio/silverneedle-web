// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Maps;
    using SilverNeedle.Serialization;

    
    public class ChooseFavoredTerrainTests
    {
        [Fact]
        public void ChooseFromFavoredTerrainList()
        {
            var forest = new TerrainType("Forest"); 
            var terrainTypes = new TerrainType[] { forest }; 
            var ctGateway = new EntityGateway<TerrainType>(terrainTypes);
            var configureStep = new ChooseFavoredTerrain(ctGateway);
            var character = new CharacterSheet();
            configureStep.Process(character, new CharacterBuildStrategy());
            
            var favTerrain = character.Get<FavoredTerrain>();
            Assert.Contains(forest, favTerrain.TerrainTypes);
        }
        
        [Fact]
        public void AddingASecondTerrainAppendsADifferentOneToTheList()
        {
            var forest = new TerrainType("Forest"); 
            var jungle = new TerrainType("Jungle");
            var terrainTypes = new TerrainType[] { forest, jungle }; 
            var ctGateway = new EntityGateway<TerrainType>(terrainTypes);
            var configureStep = new ChooseFavoredTerrain(ctGateway);
            var character = new CharacterSheet();
            configureStep.Process(character, new CharacterBuildStrategy());
            configureStep.Process(character, new CharacterBuildStrategy());
            
            var favTerrain = character.Get<FavoredTerrain>();
            Assert.Contains(forest, favTerrain.TerrainTypes);
            Assert.Contains(jungle, favTerrain.TerrainTypes);

        }
    }
}