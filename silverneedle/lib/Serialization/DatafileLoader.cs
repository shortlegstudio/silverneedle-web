// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Serialization
{
    using System;
    using System.Collections.Generic;

    public class DatafileLoader
    {
        private static DatafileLoader globalInstance; 

        public static DatafileLoader Instance {
            get {
                if (globalInstance == null) {
                    globalInstance = new DatafileLoader();
                }
                return globalInstance;
            }
        }

        private Dictionary<string, IList<IObjectStore>> fileListMap;
        private string dataDirectory;

        public DatafileLoader()
        {
            fileListMap = new Dictionary<string, IList<IObjectStore>>();
            dataDirectory = Configuration.DataPath;
            LoadDataDirectory();
        }

        public IEnumerable<IObjectStore> GetDataFiles(string datafileType)
        {
            return fileListMap[datafileType];
        }

        public IEnumerable<IObjectStore> GetDataFiles<T>()
        {
            var type = typeof(T);
            if (!fileListMap.ContainsKey(type.FullName))
            {
                ShortLog.ErrorFormat("Could not find data file of type: {0}", type.FullName);
                throw MissingDataFileException.MissingDataFilesForType(type.FullName);
            }
            return fileListMap[type.FullName];
        }

        private void LoadDataDirectory() 
        {
            // Find all yaml files and track them
            var files = System.IO.Directory.EnumerateFiles(dataDirectory, "*.yml", System.IO.SearchOption.AllDirectories);
            foreach(var f in files)
            {
                try
                {
                    var yaml = FileHelper.OpenYaml(f);
                    var type = GuessFileType(yaml);
                    ShortLog.DebugFormat("FILE: {0}, TYPE: {1}", f, type);

                    if(string.IsNullOrEmpty(type) == false)
                    {
                        IList<IObjectStore> dataFiles;
                        if (fileListMap.ContainsKey(type)) {
                            dataFiles = fileListMap[type];
                        } else {
                            dataFiles = new List<IObjectStore>();
                            fileListMap.Add(type, dataFiles);
                        }

                        dataFiles.Add(yaml);
                    }
                }
                catch(Exception e)
                {
                    ShortLog.ErrorFormat("FILE: {0} Failed to load", f);
                    ShortLog.Error(e.ToString());
                }
            }
        }

        private string GuessFileType(YamlObjectStore yaml)
        {
            return yaml.GetTag();
        }
    } 
}