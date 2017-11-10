// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System;
    using System.Linq;
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

        public static string Titlize(this string source)
        {
            return Inflector.Titleize(source);
        }

        public static bool EqualsIgnoreCase(this string source, string compare)
        {
            return string.Equals(source, compare, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ContainsIgnoreCase(this string source, string searchFor)
        {
            return source.IndexOf(searchFor, 0, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Parses by new line and comma, trims resulting strings.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] ParseList(this string source)
        {
            var array = Regex.Split(source, "[\\n,]+");
            return array.Select(x => x.Trim()).Where(x => string.IsNullOrEmpty(x) == false).ToArray();
        }

        public static int ToInteger(this string source)
        {
            return int.Parse(source);
        }

        public static string Formatted(this string format, params object[] values)
        {
            return string.Format(format, values);
        }
    }
}