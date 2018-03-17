// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MemoryStore : IObjectStore
    {
        public string Key { get; private set; }

        public string Value { get; private set; }

        public IEnumerable<string> Keys {
            get {
                return dataStore.Keys;
            }
        }

        public IEnumerable<IObjectStore> Children { 
            get { return childList; }
        }

        public bool HasChildren {
            get {
                return childList.Count > 0;
            }
        } 

        public MemoryStore()
        {
            dataStore = new Dictionary<string, MemoryStore>();
            childList = new List<MemoryStore>();
        }

        public MemoryStore(string key, string value) : this()
        {
            Key = key;
            Value = value;
            dataStore[key] = this;
        }

        public MemoryStore(string key, IEnumerable<string> list) : this()
        {
            Key = key;
            dataStore[key] = this;
            foreach(var s in list)
            {
                AddListItem(new MemoryStore("list-item", s));
            }
        }

        public IDictionary<string, string> ChildrenToDictionary()
        {
            throw new NotImplementedException();
        }

        public bool GetBool(string key)
        {
            return Boolean.Parse(this.GetString(key));
        }

        public bool GetBoolOptional(string key, bool defaultValue = false)
        {
            if(HasKey(key))
                return GetBool(key);

            return defaultValue;
        }

        public T GetEnum<T>(string key)
        {
            return (T)Enum.Parse(typeof(T), GetString(key), true);
        }

        public float GetFloat(string key)
        {
            return float.Parse(GetString(key));
        }

        public float GetFloatOptional(string key, float defaultValue = 0)
        {
            throw new NotImplementedException();
        }

        public int GetInteger(string key)
        {
            return int.Parse(GetString(key));
        }

        public int GetIntegerOptional(string key, int defaultValue = 0)
        {
            throw new NotImplementedException();
        }

        public string[] GetList(string key)
        {
            var items = GetObjectList(key);
            return items.OfType<MemoryStore>().Select(x => x.Value).ToArray();
        }

        public string[] GetListOptional(string key)
        {
            if(HasKey(key))
                return GetList(key);
            
            return new string[] { };
        }

        public IObjectStore GetObject(string key)
        {
            return dataStore[key];
        }

        public IObjectStore GetObjectOptional(string key)
        {
            if(HasKey(key))
                return GetObject(key);
            
            return null;
        }

        public string GetString(string key)
        {
            if(key == Key)
                return Value;
                
            return dataStore[key].Value;
        }

        public string GetStringOptional(string key, string defaultValue = null)
        {
            if(HasKey(key))
                return GetString(key);
            
            return defaultValue;            
        }

        public bool HasKey(string key)
        {
            return dataStore.ContainsKey(key) || string.Equals(this.Key, key);
        }

        public void SetValue(string key, string value)
        {
            SetValue(key, new MemoryStore(key, value));
            
        }

        public void SetValue(string key, int value)
        {
            SetValue(key, value.ToString());
        }

        public void SetValue(string key, IObjectStore data) {
            dataStore.Add(key, data as MemoryStore);
        }

        public void SetValue(string key, float value)
        {
            SetValue(key, value.ToString());
        }

        public void SetValue(string key, bool boolean)
        {
            SetValue(key, boolean.ToString());
        }

        public void AddListItem(IObjectStore childItem)
        {
            childList.Add(childItem as MemoryStore);
        }

        public void SetValue(string key, IEnumerable<string> values)
        {
            var obj = new MemoryStore();
            obj.Key = key;
            foreach(var v in values)
            {
                obj.AddListItem(new MemoryStore("list-item", v));
            }
            SetValue(key, obj);
        }

        public IEnumerable<IObjectStore> GetObjectList(string key)
        {
            return ((MemoryStore)GetObject(key)).Children;
        }

        public IEnumerable<IObjectStore> GetObjectListOptional(string key)
        {
            if(HasKey(key))
                return GetObjectList(key);

            return new List<IObjectStore>();
        }


        private Dictionary<string, MemoryStore> dataStore;
        private IList<MemoryStore> childList;

    }
}