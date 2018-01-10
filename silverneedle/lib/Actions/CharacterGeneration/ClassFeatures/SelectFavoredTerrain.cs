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

    public class SelectFavoredTerrain : ICharacterDesignStep, ICharacterFeatureCommand
    {
        private EntityGateway<TerrainType> terrainTypeGateway;

        public SelectFavoredTerrain()
        {
            this.terrainTypeGateway = GatewayProvider.Get<TerrainType>();
        }

        public SelectFavoredTerrain(EntityGateway<TerrainType> ctGateway)
        {
            this.terrainTypeGateway = ctGateway;
        }
        
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var fav = components.Get<FavoredTerrain>();
            if(fav == null)
            {
                fav = new FavoredTerrain();
                components.Add(fav);
            }
            fav.Add(this.terrainTypeGateway.Where(x => !fav.TerrainTypes.Contains(x)).ChooseOne());
        }
    }
}