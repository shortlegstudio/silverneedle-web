// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using SilverNeedle.Serialization;

    public static class CodeReadabilityExtensions
    {
        public static T SafeLoad<T>(this IObjectStore data)
        {
            if(data == null)
                return default(T);

            return Reflector.Instantiate<T>(typeof(T), data);
        }
    }
}