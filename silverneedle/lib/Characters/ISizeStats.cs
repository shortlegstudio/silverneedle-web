//-----------------------------------------------------------------------
// <copyright file="ISizeStats.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    /// <summary>
    /// Represents the size stats for a character.
    /// </summary>
    public interface ISizeStats
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The character size.</value>
        CharacterSize Size { get; }

        /// <summary>
        /// Gets the size modifier.
        /// </summary>
        /// <value>The size modifier.</value>
        int SizeModifier { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        int Height { get; }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <value>The weight.</value>
        int Weight { get; }

        /// <summary>
        /// Sets the size of the character.
        /// </summary>
        /// <param name="size">Size category of character.</param>
        /// <param name="height">Height of character.</param>
        /// <param name="weight">Weight of character.</param>
        void SetSize(CharacterSize size, int height, int weight);
    }
}