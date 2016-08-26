//-----------------------------------------------------------------------
// <copyright file="WeaponTrainingLevel.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Equipment
{
    /// <summary>
    /// Weapon training level.
    /// </summary>
    public enum WeaponTrainingLevel
    {
        /// <summary>
        /// Simple weapons that don't require specialized training
        /// </summary>
        Simple,

        /// <summary>
        /// Weapons that military characters would know
        /// </summary>
        Martial,

        /// <summary>
        /// Specialized weapons that only specific characters would know
        /// </summary>
        Exotic
    }
}