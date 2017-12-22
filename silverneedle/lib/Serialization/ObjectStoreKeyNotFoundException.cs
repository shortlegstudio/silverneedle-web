// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    public class ObjectStoreKeyNotFoundException : System.Exception
    {
        public ObjectStoreKeyNotFoundException(IObjectStore store, string keyName, System.Exception innerException) 
            : base(string.Format("Object Store could not find key: {0} in Object: {1}", keyName, store.ToString()), innerException)
        {
        }
    }
}