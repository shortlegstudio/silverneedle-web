// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using SilverNeedle.Treasure;
    using SilverNeedle.Utility;

    public class AdventuringGear : IInventoryItem
    {
        public string Name { get; private set; }
        public float Weight { get; private set; }
        public int Value { get; private set; }

        public AdventuringGear(IObjectStore data)
        {
            Name = data.GetString("name");
            Weight = data.GetFloat("weight");
            Value = data.GetString("value").ToCoinValue();
        }
    }
}