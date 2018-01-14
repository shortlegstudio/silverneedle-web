// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Reflection;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public static class ObjectStoreSerializer
    {
        public static T Deserialize<T>(this IObjectStore data, T result)
        {
            var typeInfo = typeof(T);
            var properties = typeInfo.GetProperties();
            foreach(var prop in properties)
            {
                var attribute = prop.GetCustomAttribute<ObjectStoreAttribute>();
                if(attribute == null)
                    continue;
                    
                var propType = prop.PropertyType.Name.ToLower();
                object propertyValue = null;
                object defaultValue = attribute.DefaultValue;
                bool useDefault = defaultValue != null;
                string keyName = attribute.PropertyName;

                switch(propType)
                {
                    case "single":
                        if(attribute.Optional)
                            if(useDefault)
                                propertyValue = data.GetFloatOptional(keyName, (float)defaultValue);
                            else
                                propertyValue = data.GetFloatOptional(keyName);
                        else
                            propertyValue = data.GetFloat(keyName);
                        break;

                    case "int32":
                        if(attribute.Optional)
                            if(useDefault)
                                propertyValue = data.GetIntegerOptional(keyName, (int)defaultValue);
                            else
                                propertyValue = data.GetIntegerOptional(keyName);
                        else
                            propertyValue = data.GetInteger(keyName);
                        break;

                    case "string":
                        if(attribute.Optional)
                            if(useDefault)
                                propertyValue = data.GetStringOptional(keyName, defaultValue.ToString());
                            else
                                propertyValue = data.GetStringOptional(keyName);
                        else
                            propertyValue = data.GetString(keyName);
                        break;
                    
                    case "string[]":
                        if(attribute.Optional)
                            propertyValue = data.GetListOptional(keyName);
                        else
                            propertyValue = data.GetList(keyName);
                        break;
                    case "boolean":
                        if(attribute.Optional)
                            if(useDefault)
                                propertyValue = data.GetBoolOptional(keyName, (bool)defaultValue);
                            else
                                propertyValue = data.GetBoolOptional(keyName);
                        else
                                propertyValue = data.GetBool(keyName);
                        break;
                    case "movementtype":
                        //TODO: Cannot just keep adding types here, but let's see how common it is first
                        propertyValue = data.GetEnum<MovementType>(keyName);
                        break;
                    case "cup":
                        propertyValue = DiceStrings.ParseDice(data.GetString(keyName));
                        break;
                    default:
                        throw new System.NotImplementedException(string.Format("Missing case: {0}", propType));
                }
                prop.SetValue(result, propertyValue);
            }

            return result;
        }
    }
}