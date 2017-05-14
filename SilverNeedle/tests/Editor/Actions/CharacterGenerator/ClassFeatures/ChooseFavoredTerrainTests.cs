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
    using SilverNeedle.Serialization;

    [TestFixture]
    public class ChooseFavoredTerrainTests
    {
        [Test]
        public void ChooseFromFavoredTerrainList()
        {
            var forest = new TerrainType("Forest"); 
            var terrainTypes = new TerrainType[] { forest }; 
            var ctGateway = new EntityGateway<TerrainType>(terrainTypes);
            var configureStep = new ChooseFavoredTerrain(ctGateway);
            var character = new CharacterSheet();
            configureStep.Process(character, new CharacterBuildStrategy());
            
            var favTerrain = character.Get<FavoredTerrain>();
            Assert.That(favTerrain.TerrainTypes, Contains.Item(forest));
        }
        
        [Test]
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
            Assert.That(favTerrain.TerrainTypes, Contains.Item(forest));
            Assert.That(favTerrain.TerrainTypes, Contains.Item(jungle));

        }
    }
}