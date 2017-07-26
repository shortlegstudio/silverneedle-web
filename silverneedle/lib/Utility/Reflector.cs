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
    using Microsoft.Extensions.DependencyModel;

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
            if(type == null)
                throw new ArgumentNullException("type");

            if(constructor.Length > 0)
            {
                var types = constructor.Select(x => x.GetType()).ToArray();
                ShortLog.DebugFormat("type is {0}", type.ToString());
                var matchConstructor = type.GetConstructor(types);

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
            // Search all loaded assemblies for the type name

            var type = FindType(typeName);
            ShortLog.DebugFormat("Type is: {0}", type.ToString());
            return type.Instantiate<T>(constructor);
        }

        public static Type FindType(string typeName)
        {
            return GetAssemblies()
                .SelectMany(x => x.ExportedTypes)
                .FirstOrDefault(t => t.FullName.Equals(typeName));
        }

        public static Assembly[] GetAssemblies()
        {
            return DependencyContext.Default.RuntimeLibraries
                .Where(x => IsCandidateCompilationLibrary(x))
                .Select(rt => Assembly.Load(new AssemblyName(rt.Name)))
                .ToArray();
        }

        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        {
            return compilationLibrary.Name.StartsWith("silverneedle")
                || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("silverneedle"));
        }
    }
}