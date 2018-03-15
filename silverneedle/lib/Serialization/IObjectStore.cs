// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Collections.Generic;

    public interface IObjectStore
    {
        bool HasKey(string key);
        IEnumerable<string> Keys { get; }
        IEnumerable<IObjectStore> Children { get; }
        bool HasChildren { get; }
        string GetString(string key);
        string GetStringOptional(string key, string defaultValue = "");
        int GetInteger(string key);
        int GetIntegerOptional(string key, int defaultValue = 0);
        float GetFloat(string key);
        float GetFloatOptional(string key, float defaultValue = 0);

        T GetEnum<T>(string key);
        bool GetBool(string key);
        bool GetBoolOptional(string key, bool defaultValue = false);

        string[] GetList(string key);
        string[] GetListOptional(string key);

        IObjectStore GetObject(string key);
        IObjectStore GetObjectOptional(string key);
    }
}