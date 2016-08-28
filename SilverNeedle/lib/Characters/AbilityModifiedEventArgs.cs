// //-----------------------------------------------------------------------
// // <copyright file="AbilityModifiedEventArgs.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ability modified event arguments are passed whenever an ability changes.
    /// </summary>
    public class AbilityModifiedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the ability that was modified
        /// </summary>
        public AbilityScore Ability { get; set; }
    }   
}