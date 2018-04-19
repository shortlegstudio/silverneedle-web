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

        IEnumerable<IObjectStore> GetObjectList(string key);
        IEnumerable<IObjectStore> GetObjectListOptional(string key);

        void SetValue(string key, string val);
        void SetValue(string key, int val);
        void SetValue(string key, IObjectStore val);
        void SetValue(string key, IEnumerable<string> vals);
        void SetValue(string key, IEnumerable<IObjectStore> vals);
        void SetValue(string key, bool v);
        void SetValue(string key, float v);
    }
}