// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Settlements
{
    using SilverNeedle.Serialization;

    public class Building : Feature, IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; private set; }

        public Building(IObjectStore configuration) : base(configuration)
        {
            configuration.Deserialize(this);
        }

        public bool Matches(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}