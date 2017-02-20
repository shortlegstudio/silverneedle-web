//-----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    using System;
    using System.Text.RegularExpressions;
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

        public static bool EqualsIgnoreCase(this string source, string compare)
        {
            return string.Equals(source, compare, StringComparison.OrdinalIgnoreCase);
        }

        public static string[] ParseList(this string source)
        {
            return Regex.Split(source, "[\\n ,]+");
        }
    }
}