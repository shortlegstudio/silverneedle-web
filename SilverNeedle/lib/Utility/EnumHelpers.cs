//-----------------------------------------------------------------------
// <copyright file="EnumHelpers.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extension methods and helpers for Enums
    /// </summary>
    public static class EnumHelpers 
    {
        /// <summary>
        /// Returns the values of an enum into an enumerable list
        /// </summary>
        /// <returns>The values to parse</returns>
        /// <typeparam name="T">The type of the enum</typeparam>
        public static IEnumerable<T> GetValues<T>() 
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Chooses a random enum value
        /// </summary>
        /// <returns>The selected enum value</returns>
        /// <typeparam name="T">The enum to choose from</typeparam>
        public static T ChooseOne<T>() 
        {
            return GetValues<T>().ToList().ChooseOne();
        }

        /// <summary>
        /// Tries to parse the enum for a value. 
        /// </summary>
        /// <returns><c>true</c>, if parse was successful, <c>false</c> otherwise.</returns>
        /// <param name="value">The value to try and parse from the enum</param>
        /// <param name="ignorecase">If set to <c>true</c> ignorecase.</param>
        /// <param name="parsed">The output parameter of the parsed value</param>
        /// <typeparam name="T">The enum type</typeparam>
        public static bool TryParse<T>(string value, bool ignorecase, out T parsed) 
        {
            parsed = default(T);
            try 
            {
                var result = Enum.Parse(typeof(T), value, ignorecase);
                parsed = (T)result;
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
