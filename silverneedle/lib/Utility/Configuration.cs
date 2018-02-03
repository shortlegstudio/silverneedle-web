// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System;
    public static class Configuration
    {
        static Configuration() 
        {
            DataPath = "../silverneedle/data";
            if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES")))
            {
                DataPath = System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES");
            }

            LogLevel = ShortLog.LogLevel.DEBUG;
            if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable(ShortLog.LOG_LEVEL_ENV)))
            {
                LogLevel = (ShortLog.LogLevel)Enum.Parse(typeof(ShortLog.LogLevel), Environment.GetEnvironmentVariable(ShortLog.LOG_LEVEL_ENV));
            }
        }

        public static string DataPath { get; set; }
        public static ShortLog.LogLevel LogLevel { get; set; }
    }
    
}