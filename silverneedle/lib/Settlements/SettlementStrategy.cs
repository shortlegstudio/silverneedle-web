// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Settlements
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class SettlementStrategy : IGatewayObject
    {
        [ObjectStore("name")] 
        public string Name { get; set; }
        [ObjectStore("minimum-population")]
        public int MinimumPopulation { get; set; }

        [ObjectStore("maximum-population")]
        public int MaximumPopulation { get; set; }
        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}