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
            get { throw new NotImplementedException(); }
        }

        public bool HasChildren {
            get {
                throw new NotImplementedException();
            }
        } 

        public MemoryStore()
        {
            dataStore = new Dictionary<string, MemoryStore>();
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
            throw new NotImplementedException();
        }

        public bool GetBoolOptional(string key)
        {
            throw new NotImplementedException();
        }

        public T GetEnum<T>(string key)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string[] GetListOptional(string key)
        {
            throw new NotImplementedException();
        }

        public IObjectStore GetObject(string key)
        {
            throw new NotImplementedException();
        }

        public IObjectStore GetObjectOptional(string key)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
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
            dataStore.Add(key, new MemoryStore(key, value));
        }

        public void SetValue(string key, int value)
        {
            SetValue(key, value.ToString());
        }

        private Dictionary<string, MemoryStore> dataStore;
    }
}