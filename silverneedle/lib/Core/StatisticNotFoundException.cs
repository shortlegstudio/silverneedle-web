// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    public class StatisticNotFoundException : System.Exception
    {
        public StatisticNotFoundException(string statistic) : base(string.Format("Could not find statistic: \"{0}\"", statistic)) 
        { 

        }
    }
}