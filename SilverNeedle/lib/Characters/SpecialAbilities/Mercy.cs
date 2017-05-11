// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class Mercy : IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }

        [ObjectStore("level")]
        public int Level { get; set; }

        public Mercy() { }
        public Mercy(string name, int level)
        {
            this.Name = name;
            this.Level = level;
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}