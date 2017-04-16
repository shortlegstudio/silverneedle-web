// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using System.Collections.Generic;
    using System.Linq;

    public class ComponentBag
    {
        public IList<object> All { get { return this.components; } }

        private List<object> components;

        public ComponentBag()
        {
            components = new List<object>();
        }

        public void Add(object obj)
        {
            components.Add(obj);
        }

        public T Get<T>()
        {
            return components.OfType<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return components.OfType<T>();
        }

        public void Replace<T>(T value)
        {
            var old = Get<T>();
            components.Remove(old);
            Add(value);
        }
    }
}