// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
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
        IList<IStatModifier> Modifiers { get; }
    }
}