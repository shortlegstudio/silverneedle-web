//-----------------------------------------------------------------------
// <copyright file="ShortLog.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    using System;
    
    /// <summary>
    /// A wrapper class that provides management of logging output. 
    /// During Unity Debug it should output to the console and during
    /// unit tests it should output to the standard console
    /// </summary>
    public static class ShortLog
    {
        /// <summary>
        /// Initializes static members of the <see cref="SilverNeedle.ShortLog"/> class.
        /// </summary>
        static ShortLog()
        {
        }

        /// <summary>
        /// Logs the specified error message
        /// </summary>
        /// <param name="message">Message to log to console</param>
        public static void Error(string message)
        {
            Console.WriteLine("ERROR: {0}", message);
        }

        /// <summary>
        /// Logs an error string by formatting the string
        /// </summary>
        /// <param name="format">Standard format to string.</param>
        /// <param name="args">Arguments that should be included in the formatted string.</param>
        public static void ErrorFormat(string format, params string[] args)
        {
            var s = string.Format(format, args);
            Error(s);
        }

        /// <summary>
        /// Debug logs the specified message.
        /// </summary>
        /// <param name="message">Message to write to debug output.</param>
        public static void Debug(string message)
        {
            Console.WriteLine("DEBUG: {0}", message);
        }

        /// <summary>
        /// Logs the debug output in standard formatted format
        /// </summary>
        /// <param name="format">Standard format to string</param>
        /// <param name="args">Arguments to be included in the format</param>
        public static void DebugFormat(string format, params string[] args)
        {
            var s = string.Format(format, args);
            Debug(s);
        }
    }
}