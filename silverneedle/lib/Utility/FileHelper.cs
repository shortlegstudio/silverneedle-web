﻿// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.IO;
    using YamlDotNet.RepresentationModel;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Provides helper methods for loading and accessing files
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Loads a Yaml document and returns the YamlDotNet parser
        /// 
        /// This is based on the data file path configured
        /// </summary>
        /// <returns>The YamlDotNet parser</returns>
        /// <param name="fileName">File name to open</param>
        public static YamlObjectStore OpenYamlDataFile(string fileName)
        {
            var path = Path.Combine(Configuration.DataPath, fileName);
            ShortLog.DebugFormat("Loading Yaml File: {0}", path);
            using(var input = File.OpenText(path))
            {
                var yaml = new YamlStream();
                yaml.Load(input);

                // Examine the stream
                return new YamlObjectStore(yaml.Documents[0].RootNode);
            }
        }

        public static YamlDataFile OpenYaml(string fileName)
        {
            ShortLog.DebugFormat("Loading Yaml File: {0}", fileName);
            using(var input = File.OpenText(fileName))
            {
                var yaml = new YamlStream();
                yaml.Load(input);
                
                // Examine the stream
                return new YamlDataFile(yaml.Documents[0].RootNode);
            }
        }

        /// <summary>
        /// Returns all files in a path based off of the dataPath for the application
        /// </summary>
        /// <returns>The files found in the directory</returns>
        /// <param name="path">Relative path to find the files</param>
        public static string[] GetFiles(string path)
        {
            return Directory.GetFiles(Path.Combine(Configuration.DataPath, path));
        }
    }
}