// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ObjectStoreAttribute : System.Attribute
    {
        public string PropertyName { get; set; }
        public bool Optional { get; set; }
        public object DefaultValue { get; set; }

        public ObjectStoreAttribute(string name)
        {
            this.PropertyName = name;
        }

        public ObjectStoreAttribute(string name, bool optional, object defaultValue = null)
        {
            this.PropertyName = name;
            this.Optional = optional;
            this.DefaultValue = defaultValue;
        }
    }
}