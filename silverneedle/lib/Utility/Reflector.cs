// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility 
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using SilverNeedle.Serialization;
    #if SILVERNEEDLE_UNITY
    #else
    using Microsoft.Extensions.DependencyModel;
    #endif

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
                throw new ArgumentNullException(nameof(type));

            try
            {
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
            catch
            {
                ShortLog.ErrorFormat("Failed to created type: {0}", type.ToString());
                throw;
            }
        }

        public static T Instantiate<T>(this string typeName, params IObjectStore[] constructor)
        {
            // Search all loaded assemblies for the type name

            var type = FindType(typeName);
            if(type == null)
                throw new TypeNotFoundException(typeName);

            ShortLog.DebugFormat("Type is: {0}", type.ToString());
            return type.Instantiate<T>(constructor);
        }

        public static Type FindType(string typeName)
        {
            return GetAssemblies()
                .SelectMany(x => x.ExportedTypes)
                .FirstOrDefault(t => t.FullName.Equals(typeName));
        }

        public static IEnumerable<Type> FindAllTypesThatImplement<I>()
        {
            return GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(I)));

        }

        #if SILVERNEEDLE_UNITY
        public static Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
        #else
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
        #endif

        public static bool Implements(this object obj, Type testInterface)
        {
            return testInterface.IsInstanceOfType(obj);
        }


        public static void DebugReflector()
        {
            var assemblies = GetAssemblies();
            foreach(var a in assemblies)
            {
                ShortLog.DebugFormat("Loaded Assembly: {0}", a.FullName);
                foreach(var t in a.GetExportedTypes().OrderBy(x => x.FullName))
                {
                    ShortLog.DebugFormat("Exported Type: {0}", t.FullName);
                }
            }
        }

        public static bool IsDelegate(this Type t)
        {
            return t.IsGenericType && ( 
                t.GetGenericTypeDefinition() != typeof(System.Func<>) ||
                t.GetGenericTypeDefinition() != typeof(System.Func<,>)
            );
        }
    }
}