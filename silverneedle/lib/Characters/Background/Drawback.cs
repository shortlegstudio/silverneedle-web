// //-----------------------------------------------------------------------
// // <copyright file="Drawback.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Characters.Background
{
    using System.Collections.Generic;
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

