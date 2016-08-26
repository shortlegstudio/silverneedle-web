// //-----------------------------------------------------------------------
// // <copyright file="NameInformation.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using SilverNeedle.Names;

namespace SilverNeedle.Names
{
    using System;
    using System.Collections.Generic;

    public class NameInformation
    {
        public IList<string> Names { get; set; }
        public string Race { get; set; }
        public NameTypes Type { get; set; }
        public string Gender { get; set; }

        public NameInformation()
        {
            Names = new List<string>();
        }
    }
}

