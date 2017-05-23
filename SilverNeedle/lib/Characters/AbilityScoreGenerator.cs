// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializableAttribute]
    public class AbilityScoreGenerator : IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }

        [ObjectStoreAttribute("description")]
        public string Description { get; set; }

        [ObjectStoreAttribute("generator")]
        public string Generator { get; set; }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }

}