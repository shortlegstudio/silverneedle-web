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
        public string Name { get; private set; }
        [ObjectStore("minimum-population")]
        public int MinimumPopulation { get; private set; }

        [ObjectStore("maximum-population")]
        public int MaximumPopulation { get; private set; }
        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}