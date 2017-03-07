using System;
using System.Collections.Generic;

namespace SilverNeedle.Utility
{
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
            dataStore = new Dictionary<string, IObjectStore>();
            childList = new List<IObjectStore>();
        }

        public MemoryStore(string key, string value) : base()
        {
            Key = key;
            Value = value;
        }

        public IDictionary<string, string> ChildrenToDictionary()
        {
            throw new NotImplementedException();
        }

        public bool GetBool(string key)
        {
            return Boolean.Parse(GetString(key));
        }

        public bool GetBoolOptional(string key)
        {
            throw new NotImplementedException();
        }

        public T GetEnum<T>(string key)
        {
            return (T)Enum.Parse(typeof(T), GetString(key));
        }

        public float GetFloat(string key)
        {
            throw new NotImplementedException();
        }

        public float GetFloatOptional(string key)
        {
            throw new NotImplementedException();
        }

        public int GetInteger(string key)
        {
            return int.Parse(GetString(key));
        }

        public int GetIntegerOptional(string key)
        {
            throw new NotImplementedException();
        }

        public string[] GetList(string key)
        {
            return GetString(key).ParseList();
        }

        public string[] GetListOptional(string key)
        {
            throw new NotImplementedException();
        }

        public IObjectStore GetObject(string key)
        {
            return dataStore[key];
        }

        public IObjectStore GetObjectOptional(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            if(key == Key)
                return Value;
                
            return dataStore[key].Value;
        }

        public string GetStringOptional(string key)
        {
            if(HasKey(key))
                return GetString(key);
            
            return "";            
        }

        public bool HasKey(string key)
        {
            return dataStore.ContainsKey(key);
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
            dataStore.Add(key, data);
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
            childList.Add(childItem);
        }

        private Dictionary<string, IObjectStore> dataStore;
        private IList<IObjectStore> childList;

    }
}