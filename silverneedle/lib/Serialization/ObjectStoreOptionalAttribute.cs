// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    public class ObjectStoreOptionalAttribute : ObjectStoreAttribute
    {
        public ObjectStoreOptionalAttribute(string name) : base(name, true)
        {

        }

        public ObjectStoreOptionalAttribute(string name, object val) : base(name, true, val)
        {

        }
    }
}