// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using SilverNeedle;
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    public class JsonValueStatisticConverter : JsonConverter<IValueStatistic>
    {
        public override IValueStatistic ReadJson(JsonReader reader, Type objectType, IValueStatistic existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, IValueStatistic value, JsonSerializer serializer)
        {
            var temp = new {
                Name = value.Name,
                Value = value.TotalValue
            };
            var token = JObject.FromObject(temp);
            token.WriteTo(writer);
        }
    }
}