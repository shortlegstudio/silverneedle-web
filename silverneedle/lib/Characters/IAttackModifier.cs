//-----------------------------------------------------------------------
// <copyright file="IAttackModifier.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    /// <summary>
    /// Represents an attack modifier.
    /// </summary>
    public interface IAttackModifier
    {
        /// <summary>
        /// Gets the modifier amount.
        /// </summary>
        /// <returns>The modifier amount.</returns>
        int GetModifier();
    }
}