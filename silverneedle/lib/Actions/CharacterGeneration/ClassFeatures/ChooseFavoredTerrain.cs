// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Maps;
    using SilverNeedle.Serialization;

    public class ChooseFavoredTerrain : ICharacterDesignStep
    {
        private EntityGateway<TerrainType> terrainTypeGateway;

        public ChooseFavoredTerrain()
        {
            this.terrainTypeGateway = GatewayProvider.Get<TerrainType>();
        }

        public ChooseFavoredTerrain(EntityGateway<TerrainType> ctGateway)
        {
            this.terrainTypeGateway = ctGateway;
        }
        
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var fav = character.Get<FavoredTerrain>();
            if(fav == null)
            {
                fav = new FavoredTerrain();
                character.Add(fav);
            }
            fav.Add(this.terrainTypeGateway.Where(x => !fav.TerrainTypes.Contains(x)).ChooseOne());
        }
    }
}