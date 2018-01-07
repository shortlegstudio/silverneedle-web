// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    public static class ObjectExtensions
    {
        public static T Default<T>(this T val, T defIfNull)
        {
            if(val == null)
                return defIfNull;
            
            return val;
        }
    }
}