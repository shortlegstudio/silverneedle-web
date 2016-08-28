// //-----------------------------------------------------------------------
// // <copyright file="Homeland.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SilverNeedle.Characters.Background
{
    public class Homeland
    {
        public Homeland()
        {
            Traits = new List<string>();
        }
        public string Race { get; set; }
        public string Location { get; set; }
        public int Weighting { get; set; }
        public IList<string> Traits { get; private set; }
    }
}

