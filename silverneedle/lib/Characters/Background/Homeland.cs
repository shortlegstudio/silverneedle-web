// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    public class Homeland
    {
        public Homeland()
        {
            Traits = new List<string>();
        }

        public Homeland(string race, IObjectStore data) : this()
        {
            this.Race = race;
            this.Location = data.GetString("location");
            this.Weighting = data.GetInteger("weight");
            this.Traits.Add(data.GetListOptional("traits"));
            
        }

        public string Race { get; set; }
        public string Location { get; set; }
        public int Weighting { get; set; }
        public IList<string> Traits { get; private set; }
    }
}

