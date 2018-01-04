// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

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

        public static T EnumValue<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
