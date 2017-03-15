//-----------------------------------------------------------------------
// <copyright file="Homeland.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters.Background
{
    using System.Collections.Generic;
    using SilverNeedle.Utility;

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

