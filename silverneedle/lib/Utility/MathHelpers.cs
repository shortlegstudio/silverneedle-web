// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

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

        public static int AtLeast(this int val, int min)
        {
            return (int)Math.Max(val, min);
        }

        public static int Ceiling(this float val)
        {
            return (int)Math.Ceiling(val);
        }
    }
}