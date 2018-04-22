// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    public class JsonIgnoreClassConverter : JsonConverter
    {
        private Type[] ignoreTypes = new Type[] {
            typeof(YamlDotNet.RepresentationModel.YamlDocument),
            typeof(YamlDotNet.RepresentationModel.YamlNode),
            typeof(YamlDotNet.RepresentationModel.YamlScalarNode),
            typeof(YamlDotNet.RepresentationModel.YamlSequenceNode),
            typeof(YamlDotNet.RepresentationModel.YamlMappingNode)
        };
        public override bool CanConvert(Type objectType)
        {
            return ignoreTypes.Contains(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;    
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ShortLog.DebugFormat("Ignoring class: {0} ({1})", value.GetType().FullName, value.ToString()); 
        }
    }
}