// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.IO;
    using YamlDotNet.RepresentationModel;

    /// <summary>
    /// Helper methods for dealing with Yaml
    /// </summary>
    public static class YamlHelpers
    {
        /// <summary>
        /// Parses a specific string into YamlNode
        /// </summary>
        /// <returns>The yaml parsed into an accessible class</returns>
        /// <param name="yamlString">Yaml string to parse out</param>
        public static YamlObjectStore ParseYaml(this string yamlString)
        {
            var input = new StringReader(yamlString);
            var yaml = new YamlStream();
            yaml.Load(input);
            return new YamlObjectStore(yaml.Documents[0].RootNode);
        }

        public static string WriteToString(this YamlObjectStore yaml)
        {
            var doc = new YamlDotNet.RepresentationModel.YamlDocument(yaml.MappingNode);
            var sb = new System.Text.StringBuilder();
            var strm = new YamlDotNet.RepresentationModel.YamlStream();
            strm.Add(doc);
            strm.Save(new System.IO.StringWriter(sb));
            return sb.ToString();
        }
    }
}