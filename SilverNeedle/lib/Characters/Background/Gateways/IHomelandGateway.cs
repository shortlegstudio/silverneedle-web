// //-----------------------------------------------------------------------
// // <copyright file="IHomelandGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Characters.Background
{
    using SilverNeedle.Utility;

    public interface IHomelandGateway
    {
        WeightedOptionTable<Homeland> GetRacialOptions(string race);
    }
}

