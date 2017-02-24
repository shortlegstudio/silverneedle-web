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

        public T ChooseOne()
        {
            return dataStore.ChooseOne();
        }

        public IEnumerable<T> Choose(int number)
        {
            return dataStore.Choose(number);
        }

        public EntityGateway(IEnumerable<IObjectStore> data) 
        {
            dataStore = new List<T>();
            objectType = typeof(T);
            LoadObjects(data);
        }

        public EntityGateway() 
        {
            dataStore = new List<T>();
            objectType = typeof(T);
            var files = DatafileLoader.Instance.GetDataFiles<T>();
            foreach(var f in files)
            {
                //NOTE: Root of a data file is the file itself not the objects contained
                //Kind of a tricky distinction, probably need a different data type
                //in the future
                LoadObjects(f.Children);
            }
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