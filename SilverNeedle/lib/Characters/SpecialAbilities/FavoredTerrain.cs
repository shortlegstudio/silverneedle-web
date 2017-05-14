// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using SilverNeedle.Maps;
    public class FavoredTerrain : SpecialAbility
    {
        private IDictionary<TerrainType, int> terrains;
        public FavoredTerrain() 
        {
            terrains = new Dictionary<TerrainType, int>();
        }
        public IEnumerable<TerrainType> TerrainTypes { get { return terrains.Keys; } }
        public int Bonus(TerrainType type) 
        {
            return terrains[type];
        }
        public void Add(TerrainType additionalType)
        {
            terrains.Add(additionalType, 2);
        }

        public void EnhanceBonus(TerrainType type)
        {
            terrains[type] += 2;
        }

        public override string Name
        {
            get
            {
                
                var buildCharacterList = new List<string>();
                foreach(var type in terrains.Keys)
                {
                    buildCharacterList.Add(string.Format("{0} {1}", type.Name, Bonus(type).ToModifierString()));
                }
                return string.Format("Favored Terrain ({0})", string.Join(", ", buildCharacterList));
            }
        }
    }
}