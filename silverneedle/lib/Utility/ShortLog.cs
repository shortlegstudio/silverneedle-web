// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System;
    using System.Linq;
    
    /// <summary>
    /// A wrapper class that provides management of logging output. 
    /// During Unity Debug it should output to the console and during
    /// unit tests it should output to the standard console
    /// </summary>
    public static class ShortLog
    {

        public const string LOG_LEVEL_ENV = "SILVERNEEDLE_LOG_LEVEL";
        /// <summary>
        /// Initializes static members of the <see cref="SilverNeedle.ShortLog"/> class.
        /// </summary>
        static ShortLog()
        {
        }

        public static LogLevel GetLogLevel()
        {
            return Configuration.LogLevel;
        }

        /// <summary>
        /// Logs the specified error message
        /// </summary>
        /// <param name="message">Message to log to console</param>
        public static void Error(string message)
        {
            WriteFormat(LogLevel.ERROR, message);
        }

        /// <summary>
        /// Logs an error string by formatting the string
        /// </summary>
        /// <param name="format">Standard format to string.</param>
        /// <param name="args">Arguments that should be included in the formatted string.</param>
        public static void ErrorFormat(string format, params string[] args)
        {
            WriteFormat(LogLevel.ERROR, format, args);
        }

        /// <summary>
        /// Debug logs the specified message.
        /// </summary>
        /// <param name="message">Message to write to debug output.</param>
        public static void Debug(string message)
        {
            WriteFormat(LogLevel.DEBUG, message);
        }

        /// <summary>
        /// Logs the debug output in standard formatted format
        /// </summary>
        /// <param name="format">Standard format to string</param>
        /// <param name="args">Arguments to be included in the format</param>
        public static void DebugFormat(string format, params object[] args)
        {
            WriteFormat(LogLevel.DEBUG, format, args.Select(x => x.ToString()).ToArray());
        }
        public static void WarnFormat(string format, params string[] args)
        {
            WriteFormat(LogLevel.WARN, format, args);
        }

        public static void Warn(string message)
        {
            WriteFormat(LogLevel.WARN, message);
        }

        private const string log_format = "{0} - {1}: {2}";
        private static void WriteFormat(LogLevel level, string format, params string[] args)
        {
            if (GetLogLevel() >= level)
            {
                string message = string.Format(format, args);
                Console.WriteLine(log_format, DateTime.Now.ToString(), level, message);
            }
        }

        public enum LogLevel
        {
            ERROR = 0,
            WARN = 3,
            DEBUG = 10
        }
    }
}