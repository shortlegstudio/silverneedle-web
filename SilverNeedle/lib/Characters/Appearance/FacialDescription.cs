// //-----------------------------------------------------------------------
// // <copyright file="FacialDescription.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;

namespace SilverNeedle.Characters.Appearance
{
    public class FacialDescription
    {
        public FacialDescription()
        {
        }

        public EyeColors EyeColor { get; set; }
        public FacialHairStyles FacialHair { get; set; }
        public HairColors HairColor { get; set; }
        public HairStyles HairStyle { get; set; }
    }
}

