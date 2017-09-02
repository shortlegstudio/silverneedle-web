// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [ObjectStoreSerializable]
    public class BirthCircumstance : IWeightedTableObject, IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }

        [ObjectStore("weight")]
        public int Weighting { get; set; }

        [ObjectStoreOptional("tags")]
        public string[] Tags { get; set; }

        public BirthCircumstance() { }
        public BirthCircumstance(string name, int weight)
        {
            this.Name = name;
            this.Weighting = weight;
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}