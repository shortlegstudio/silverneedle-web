// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System;
    using SilverNeedle.Treasure;
    using SilverNeedle.Utility;
    
    public class Gear : IGear, IGatewayObject
    {
        public string Name { get; private set; }
        public float Weight { get; private set; }
        public int Value { get; private set; }

        public Gear(IObjectStore data)
        {
            Name = data.GetString("name");
            //ShortLog.DebugFormat("Loading Gear: {0}", Name);
            Weight = data.GetFloat("weight");
            Value = data.GetString("value").ToCoinValue();
        }

        public Gear(string name, int value, float weight)
        {
            Name = name;
            Value = value;
            Weight = weight;
        }

        public bool Matches(string name) 
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}