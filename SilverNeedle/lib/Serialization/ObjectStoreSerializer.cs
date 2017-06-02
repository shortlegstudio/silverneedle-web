// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Reflection;
    using SilverNeedle.Utility;
    public static class ObjectStoreSerializer
    {
        public static T Deserialize<T>(this IObjectStore data, T result)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var properties = typeInfo.GetProperties();
            foreach(var prop in properties)
            {
                var attribute = prop.GetCustomAttribute<ObjectStoreAttribute>();
                if(attribute == null)
                    continue;
                    
                var propType = prop.DeclaringType;
                object propertyValue = null;
                switch(prop.PropertyType.Name.ToLower())
                {
                    case "single":
                        if(attribute.Optional)
                            propertyValue = data.GetFloatOptional(attribute.PropertyName);
                        else
                            propertyValue = data.GetFloat(attribute.PropertyName);
                        break;

                    case "int32":
                        if(attribute.Optional)
                            propertyValue = data.GetIntegerOptional(attribute.PropertyName);
                        else
                            propertyValue = data.GetInteger(attribute.PropertyName);
                        break;

                    case "string":
                        if(attribute.Optional)
                            propertyValue = data.GetStringOptional(attribute.PropertyName);
                        else
                            propertyValue = data.GetString(attribute.PropertyName);
                        break;
                    
                    case "string[]":
                        if(attribute.Optional)
                            propertyValue = data.GetListOptional(attribute.PropertyName);
                        else
                            propertyValue = data.GetList(attribute.PropertyName);
                        break;
                }
                prop.SetValue(result, propertyValue);
            }

            return result;
        }
    }
}