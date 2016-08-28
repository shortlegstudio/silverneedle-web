// //-----------------------------------------------------------------------
// // <copyright file="History.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;

namespace SilverNeedle.Characters.Background
{
    public class History
    {
        public History()
        {
        }

        public Homeland Homeland { get; set; }
        public FamilyTree FamilyTree { get; set; }
        public ClassOrigin ClassOriginStory { get; set; }
        public Drawback Drawback { get; set; }
    }
}

