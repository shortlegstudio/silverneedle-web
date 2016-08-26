// //-----------------------------------------------------------------------
// // <copyright file="ICharacterNamesGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Names.Gateways
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters;

    public interface ICharacterNamesGateway
    {
        IList<string> GetFirstNames();
        IList<string> GetFirstNames(Gender gender, string race);
        IList<string> GetLastNames();
        IList<string> GetLastNames(string race);

    }
}

