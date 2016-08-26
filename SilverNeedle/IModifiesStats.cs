//-----------------------------------------------------------------------
// <copyright file="IModifiesStats.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an entity that can modify stats
    /// </summary>
    public interface IModifiesStats
    {
        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this class.</value>
        IList<BasicStatModifier> Modifiers { get; }
    }
}