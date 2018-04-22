// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class JsonCharacterSheetConverter : JsonConverter<CharacterSheet>
    {
        public override CharacterSheet ReadJson(JsonReader reader, Type objectType, CharacterSheet existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, CharacterSheet value, JsonSerializer serializer)
        {
            foreach(var c in value.Components.All)
            {
                JToken t = JToken.FromObject(c, serializer);
                t.WriteTo(writer);
            }
        }
    }
}