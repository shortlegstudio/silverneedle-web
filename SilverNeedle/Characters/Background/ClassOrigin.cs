// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginStory.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------


namespace SilverNeedle.Characters.Background
{
    using System;
    using System.Collections.Generic;

    public class ClassOrigin
    {
        public ClassOrigin()
        {
            Traits = new List<string>();
            Storylines = new List<string>();
        }

        public string Name { get; set; }
        public string Class { get; set; }
        public int Weighting { get; set; }
        public IList<string> Traits { get; set; }
        public IList<string> Storylines { get; set; }
    }
}

