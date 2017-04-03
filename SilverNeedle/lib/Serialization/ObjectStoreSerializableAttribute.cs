// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ObjectStoreSerializableAttribute : System.Attribute
    {
        public ObjectStoreSerializableAttribute()
        {
        }
    }
}