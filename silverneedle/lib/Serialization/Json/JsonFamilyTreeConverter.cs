// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;

    public class JsonFamilyTreeConverter : JsonConverter<FamilyTree>
    {
        public override FamilyTree ReadJson(JsonReader reader, Type objectType, FamilyTree existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, FamilyTree value, JsonSerializer serializer)
        {
            var temp = new {
                FatherName = value.FatherName,
                MotherName = value.MotherName
            };

            var token = JObject.FromObject(temp);
            token.WriteTo(writer);
        }
    }
}