// //-----------------------------------------------------------------------
// // <copyright file="CharacterSheetEventArgs.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;

    /// <summary>
    /// Character sheet event arguments.
    /// </summary>
    public class CharacterSheetEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the sheet that is involved in the event.
        /// </summary>
        /// <value>The character sheet.</value>
        public CharacterSheet Sheet { get; set; }
    }
}