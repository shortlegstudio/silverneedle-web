// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility 
{
    using System;
    using System.Linq;
    using System.Reflection;
    using SilverNeedle.Serialization;
    public static class Reflector {
        /// <summary>
        /// Instantiates an object and passes in IObjectStore information
        /// If no constructor with IObjectStore data is available but there is
        /// an empty Constructor it will utilize that
        /// </summary>
        /// <param name="type"></param>
        /// <param name="constructor"></param>
        /// <returns></returns>
        public static T Instantiate<T>(this System.Type type, params IObjectStore[] constructor) 
        {
            if(constructor.Length > 0)
            {
                var typeInfo = type.GetTypeInfo();
                var types = constructor.Select(x => x.GetType()).ToArray();
                var matchConstructor = typeInfo.GetConstructor(types);

                //Could not find a matching constructor, just try for an empty one
                if (matchConstructor == null)
                {
                    return (T)Activator.CreateInstance(type);
                }
            }
            return (T)Activator.CreateInstance(type, constructor);
        }

        public static T Instantiate<T>(this string typeName, params IObjectStore[] constructor)
        {
            var type = System.Type.GetType(typeName);
            ShortLog.DebugFormat("Type is: {0}", type.ToString());
            return type.Instantiate<T>(constructor);
        }
    }
}