// //-----------------------------------------------------------------------
// // <copyright file="Drawback.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SilverNeedle.Characters.Background
{
    public class Drawback
    {
        public Drawback()
        {
            Traits = new List<string>();
        }

        public string Name { get; set; }
        public int Weighting { get; set; }
        public IList<string> Traits { get; set; }
    }
}

