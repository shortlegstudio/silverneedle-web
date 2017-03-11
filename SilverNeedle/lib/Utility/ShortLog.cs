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
            WriteFormat("ERROR", message);
        }

        /// <summary>
        /// Logs an error string by formatting the string
        /// </summary>
        /// <param name="format">Standard format to string.</param>
        /// <param name="args">Arguments that should be included in the formatted string.</param>
        public static void ErrorFormat(string format, params string[] args)
        {
            WriteFormat("ERROR", format, args);
        }

        /// <summary>
        /// Debug logs the specified message.
        /// </summary>
        /// <param name="message">Message to write to debug output.</param>
        public static void Debug(string message)
        {
            WriteFormat("DEBUG", message);
        }

        /// <summary>
        /// Logs the debug output in standard formatted format
        /// </summary>
        /// <param name="format">Standard format to string</param>
        /// <param name="args">Arguments to be included in the format</param>
        public static void DebugFormat(string format, params string[] args)
        {
            WriteFormat("DEBUG", format, args);
        }

        public static void WarnFormat(string format, params string[] args)
        {
            WriteFormat("WARN", format, args);
        }

        public static void Warn(string message)
        {
            WriteFormat("WARN", message);
        }

        private const string log_format = "{0} - {1}: {2}";
        private static void WriteFormat(string level, string format, params string[] args)
        {
            string message = string.Format(format, args);
            Console.WriteLine(log_format, DateTime.Now.ToString(), level, message);
        }
    }
}