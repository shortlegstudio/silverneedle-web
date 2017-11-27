// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class DragonType : IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }
        [ObjectStore("energy-type")]
        public string EnergyType { get; set; }
        [ObjectStore("breath-range")]
        public string BreathRange { get; set; }
        [ObjectStore("breath-shape")]
        public string BreathShape { get; set; }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}