// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Collections.Generic;

    public interface IObjectStore
    {
        string Key { get; }
        string Value { get; }
        bool HasKey(string key);
        IEnumerable<string> Keys { get; }
        IEnumerable<IObjectStore> Children { get; }
        bool HasChildren { get; }
        string GetString(string key);
        string GetStringOptional(string key);
        int GetInteger(string key);
        int GetIntegerOptional(string key);
        float GetFloat(string key);
        float GetFloatOptional(string key);

        T GetEnum<T>(string key);
        bool GetBool(string key);
        bool GetBoolOptional(string key);

        string[] GetList(string key);
        string[] GetListOptional(string key);

        IObjectStore GetObject(string key);
        IObjectStore GetObjectOptional(string key);
        IDictionary<string, string> ChildrenToDictionary();
    }
}