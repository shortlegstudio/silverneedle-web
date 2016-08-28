//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    using Inflector;

    /// <summary>
    /// Extension methods for calling inflector
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalize the specified string.
        /// </summary>
        /// <param name="source">Source string to capitalize</param>
        /// <returns>Returns the capitalized string</returns>
        public static string Capitalize(this string source)
        {
            return Inflector.Capitalize(source);
        }
    }
}