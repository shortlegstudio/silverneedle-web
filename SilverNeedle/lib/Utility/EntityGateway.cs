// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Linq;

namespace SilverNeedle.Utility 
{
    public class EntityGateway<T> : IEntityGateway<T>
    {
        private IList<T> dataStore;
        private Type objectType;
        public IEnumerable<T> All()
        {
            return dataStore;
        }

        public IEnumerable<T> Where(Func<T, bool> predicate) 
        {
            return dataStore.Where(predicate);
        }

        public EntityGateway(IEnumerable<IObjectStore> data) 
        {
            dataStore = new List<T>();
            objectType = typeof(T);
            LoadObjects(data);
        }

        private void LoadObjects(IEnumerable<IObjectStore> data)
        {
            foreach(var d in data) {
                T obj = objectType.Instantiate<T>(d);
                dataStore.Add(obj);
            }
        }
    }

}