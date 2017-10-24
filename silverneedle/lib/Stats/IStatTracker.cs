// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that represents a class that handles some character stats
    /// </summary>
    public interface IStatTracker
    {
        /// <summary>
        /// The implementing class must handle modifiers to stats under its control
        /// </summary>
        /// <param name="modifier">Modifier for stats</param>
        void ProcessModifier(IModifiesStats modifier);

        IEnumerable<IStatistic> Statistics { get; }
    }
}