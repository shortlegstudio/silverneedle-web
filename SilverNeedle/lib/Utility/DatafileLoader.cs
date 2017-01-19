// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle
{
    using System.Collections.Generic;
    using SilverNeedle.Yaml;

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

        private Dictionary<string, IList<YamlNodeWrapper>> fileListYamlMap;
        private string dataDirectory;

        public DatafileLoader()
        {
            fileListYamlMap = new Dictionary<string, IList<YamlNodeWrapper>>();
            dataDirectory = Configuration.DataPath;
            LoadDataDirectory();
        }

        public IEnumerable<YamlNodeWrapper> GetYamlFiles(string datafileType)
        {
            return fileListYamlMap[datafileType];
        }

        private void LoadDataDirectory() 
        {
            // Find all yaml files and track them
            var files = System.IO.Directory.EnumerateFiles(dataDirectory, "*.yml", System.IO.SearchOption.AllDirectories);
            foreach(var f in files)
            {
                var yaml = FileHelper.OpenYaml(f);
                var type = GuessFileType(yaml);
                ShortLog.DebugFormat("FILE: {0}, TYPE: {1}", f, type);

                if(string.IsNullOrEmpty(type) == false)
                {
                    IList<YamlNodeWrapper> yamlFiles;
                    if (fileListYamlMap.ContainsKey(type)) {
                        yamlFiles = fileListYamlMap[type];
                    } else {
                        yamlFiles = new List<YamlNodeWrapper>();
                        fileListYamlMap.Add(type, yamlFiles);
                    }

                    yamlFiles.Add(yaml);
                }
            }
        }

        private string GuessFileType(YamlNodeWrapper yaml)
        {
            return yaml.GetTag();
        }
    } 
}