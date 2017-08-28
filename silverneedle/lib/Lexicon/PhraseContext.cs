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

        public void Add<TKey, TValue>(TKey key, TValue value) 
        {
            properties.Add(key.ToString(), value);
        }
    }
}