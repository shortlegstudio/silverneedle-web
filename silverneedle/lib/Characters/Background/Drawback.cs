// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Drawback : IGatewayObject, IWeightedTableObject
    {
        public Drawback()
        {
            Traits = new List<string>();
        }

        public Drawback(IObjectStore data) : this()
        {
            Name = data.GetString("name");
            Weighting = data.GetInteger("weight");
            Traits.Add(data.GetListOptional("traits"));    
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        public string Name { get; set; }
        public int Weighting { get; set; }
        public IList<string> Traits { get; set; }
    }
}

