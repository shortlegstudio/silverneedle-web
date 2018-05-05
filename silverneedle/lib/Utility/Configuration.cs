// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System;
    using StackExchange.Profiling;
    public static class Configuration
    {
        static Configuration() 
        {
            DataPath = "../data";
            if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES")))
            {
                DataPath = System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES");
            }

            LogLevel = ShortLog.LogLevel.WARN;
            if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable(ShortLog.LOG_LEVEL_ENV)))
            {
                LogLevel = (ShortLog.LogLevel)Enum.Parse(typeof(ShortLog.LogLevel), Environment.GetEnvironmentVariable(ShortLog.LOG_LEVEL_ENV), true);
            }
        }

        public static string DataPath { get; set; }
        public static ShortLog.LogLevel LogLevel { get; set; }

        private static MiniProfiler _profiler;
        public static MiniProfiler Profiler 
        { 
            get 
            { 
                if(_profiler == null)
                    _profiler = MiniProfiler.Current;
                return _profiler;
            } 
            set { _profiler = value; } 
        }
    }
    
}