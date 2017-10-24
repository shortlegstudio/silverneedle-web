// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    public static class Configuration
    {
        static Configuration() 
        {
            DataPath = "../silverneedle/data";
            if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES")))
            {
                DataPath = System.Environment.GetEnvironmentVariable("SILVERNEEDLE_DATAFILES");
            }
        }

        public static string DataPath { get; set; }
    }
    
}