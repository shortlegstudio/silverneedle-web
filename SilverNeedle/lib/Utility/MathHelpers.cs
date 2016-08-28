// //-----------------------------------------------------------------------
// // <copyright file="MathHelpers.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle
{
    using System; 
    
    /// <summary>
    /// Provides help wrapping Unity Math functions so libraries are not completely dependent on them
    /// </summary>
    public static class MathHelpers
    {
        /// <summary>
        /// Floors a float and converts to int.
        /// </summary>
        /// <returns>The integer representation floored to the lower value</returns>
        /// <param name="val">Value to floor.</param>
        public static int FloorToInt(this float val) 
        {
            return (int)Math.Floor(val);
        }
    }
}