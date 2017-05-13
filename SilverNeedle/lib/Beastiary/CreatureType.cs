// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Beastiary
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class CreatureType : IGatewayObject
    {
        public CreatureType() { }
        public CreatureType(string type, string subType = "")
        {
            this.Type = type;
            this.SubType = subType;
        }

        [ObjectStore("type")]
        public string Type { get; set; }

        [ObjectStore("subtype", true)]
        public string SubType { get; set; }

        public string Name { 
            get 
            { 
                if(string.IsNullOrEmpty(SubType))
                    return this.Type;
                
                return string.Format("{0} ({1})", Type, SubType);
            }
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}