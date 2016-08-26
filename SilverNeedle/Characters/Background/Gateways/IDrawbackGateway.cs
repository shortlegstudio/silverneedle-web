// //-----------------------------------------------------------------------
// // <copyright file="IDrawbackGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;

namespace SilverNeedle.Characters.Background
{
    public interface IDrawbackGateway
    {
        WeightedOptionTable<Drawback> GetDrawbacks();
        Drawback ChooseOne();
    }
}

