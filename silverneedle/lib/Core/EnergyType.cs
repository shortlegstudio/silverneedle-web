// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Core
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class EnergyType : IGatewayObject, IDamageType
    {
        [ObjectStore("name")]
        public string Name { get; set; }
        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}