// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Treasure
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class Gem : IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }


        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}