// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public static class ObjectStoreSerializer
    {
        public const string SERIALIZED_TYPE_KEY = "serialized-type";
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
                    case "spelltype":
                        propertyValue = data.GetEnum<Spells.SpellType>(keyName);
                        break;
                    case "object[]":
                        var objectList = data.GetObjectList(keyName);
                        var newList = new List<object>();
                        foreach(var o in objectList)
                        {
                            var instance = o.GetString(SERIALIZED_TYPE_KEY).Instantiate<object>(o);
                            newList.Add(instance);
                        }
                        propertyValue = newList.ToArray();
                        break;
                    default:
                        ShortLog.DebugFormat("Attempting to deserialize: {0}", propType);
                        if(prop.PropertyType.IsEnum)
                        {
                            propertyValue = System.Enum.Parse(prop.PropertyType, data.GetString(keyName), true);
                        }
                        else
                        {
                            propertyValue = DeserializeObject(data, keyName, attribute.Optional, prop.PropertyType);
                        }
                        break;
                }
                prop.SetValue(result, propertyValue);
            }

            return result;
        }

        private static object DeserializeObject(IObjectStore configuration, string keyName, bool optional, System.Type objectType)
        {
            if(optional)
            {
                var objInfo = configuration.GetObjectOptional(keyName);
                if(objInfo != null)
                    return objectType.Instantiate<object>(objInfo);
            }
            else
            {
                return objectType.Instantiate<object>(configuration.GetObject(keyName));
            }
            return null;
        }

        public static void Serialize(this IObjectStore storage, object val)
        {
            var typeInfo = val.GetType();
            var properties = typeInfo.GetProperties();
            storage.SetValue(SERIALIZED_TYPE_KEY, typeInfo.FullName);
            foreach(var prop in properties)
            {
                var attribute = prop.GetCustomAttribute<ObjectStoreAttribute>();
                if(attribute == null)
                    continue;

                var propType = prop.PropertyType.Name.ToLower();
                var propertyValue = prop.GetValue(val);
                if(propertyValue == null)
                    continue;

                switch(propType)
                {
                    case "boolean":
                        storage.SetValue(attribute.PropertyName, (bool)propertyValue);
                        break;

                    case "int32":
                        storage.SetValue(attribute.PropertyName, (int)propertyValue);
                        break;

                    case "single":
                        storage.SetValue(attribute.PropertyName, (float)propertyValue);
                        break;

                    case "string":
                        storage.SetValue(attribute.PropertyName, propertyValue.ToString());
                        break;

                    case "string[]":
                        storage.SetValue(attribute.PropertyName, (string[])propertyValue);
                        break;

                    case "object[]":
                        var objectArray = (object[])propertyValue;
                        var store = new List<YamlObjectStore>();
                        foreach(var o in objectArray)
                        {
                            var s = new YamlObjectStore();
                            s.Serialize(o);
                            store.Add(s);
                        }
                        storage.SetValue(attribute.PropertyName, store);
                        break;

                    case "cup":
                    default:
                        if(prop.PropertyType.IsEnum)
                        {
                            storage.SetValue(attribute.PropertyName, propertyValue.ToString());
                        }
                        else
                        {
                            ShortLog.DebugFormat("Attempting to serialize: {0} {1}", attribute.PropertyName, prop.PropertyType);
                            var newStore = new YamlObjectStore();
                            newStore.Serialize(propertyValue);
                            storage.SetValue(attribute.PropertyName, newStore);
                        }
                        break;
                }
            }
        }
    }
}