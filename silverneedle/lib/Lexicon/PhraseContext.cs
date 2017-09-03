// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;

    public class PhraseContext : System.Collections.IEnumerable
    {
        public const string CONTEXT_KEY = "BaseContext";
        private IDictionary<string, object> properties = new Dictionary<string, object>();

        public void Add(string name, object value)
        {
            properties.Add(name, value);
        }

        public T GetValue<T>(string name)
        {
            return (T)properties[name];
        }

        public object CreateObject()
        {
            var expand = new ExpandoObject();
            var props = (IDictionary<string, object>)expand;
            props.Add(CONTEXT_KEY, this);
            foreach(var keyvalue in properties) 
            {
                props.Add(keyvalue.Key, keyvalue.Value);
            }

            return expand;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)properties).GetEnumerator();
        }

        public void Add<TValue>(string key, TValue value) 
        {
            if(properties.ContainsKey(key))
            {
                properties[key] = value;
            }
            else
            {
                properties.Add(key, value);
            }
        }
    }
}