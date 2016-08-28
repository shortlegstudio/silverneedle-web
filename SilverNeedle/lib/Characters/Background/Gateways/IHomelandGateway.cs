// //-----------------------------------------------------------------------
// // <copyright file="IHomelandGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace SilverNeedle.Characters.Background
{
    public interface IHomelandGateway
    {
        WeightedOptionTable<Homeland> GetRacialOptions(string race);
    }
}

