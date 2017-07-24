// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Maps
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class TerrainType : IGatewayObject
    {
        public TerrainType() { }
        public TerrainType(string name)
        {
            this.Name = name; 
        }

        [ObjectStoreAttribute("name")]
        public string Name { get; set; }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}