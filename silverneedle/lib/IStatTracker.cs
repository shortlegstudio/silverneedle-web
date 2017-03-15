//-----------------------------------------------------------------------
// <copyright file="IStatTracker.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    using SilverNeedle.Characters;

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
    }
}