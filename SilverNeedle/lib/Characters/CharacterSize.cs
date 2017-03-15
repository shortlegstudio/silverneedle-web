//-----------------------------------------------------------------------
// <copyright file="CharacterSize.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    /// <summary>
    /// Character size modifiers.
    /// </summary>
    public enum CharacterSize
    {
        /// <summary>
        /// A colossal creatures
        /// </summary>
        Colossal = -8,

        /// <summary>
        /// A gargantuan creature
        /// </summary>
        Gargantuan = -4,

        /// <summary>
        /// A huge creature
        /// </summary>
        Huge = -2,

        /// <summary>
        /// A large creature.
        /// </summary>
        Large = -1,

        /// <summary>
        /// A medium creature.
        /// </summary>
        Medium = 0,

        /// <summary>
        /// A small creature.
        /// </summary>
        Small = 1,

        /// <summary>
        /// A tiny creature.
        /// </summary>
        Tiny = 2,

        /// <summary>
        /// A diminutive creature.
        /// </summary>
        Diminutive = 4,

        /// <summary>
        /// A fine creature. 
        /// </summary>
        Fine = 8
    }
}