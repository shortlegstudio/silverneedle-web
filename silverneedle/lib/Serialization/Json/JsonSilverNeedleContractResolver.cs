// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using SilverNeedle.Utility;

    public class JsonSilverNeedleContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
        {
            var props = base.CreateProperties(type, memberSerialization);
            return props.Where(
                p => ShouldSerializeProperty(p)
            ).ToList();
        }
        private bool ShouldSerializeProperty(JsonProperty property)
        {
            if(property.PropertyType.IsDelegate())
                return false;
            
            if(property.PropertyType == typeof(ComponentContainer) &&
                property.PropertyName == "Parent")
                return false;

            return true;
        }
    }
}