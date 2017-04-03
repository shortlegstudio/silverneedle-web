// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SilverNeedle.Utility;

namespace SilverNeedle.Serialization 
{
    public class EntityGateway<T> : IEntityGateway<T> where T : IGatewayObject
    {
        private IList<T> dataStore;
        private Type objectType;
        public IEnumerable<T> All()
        {
            return dataStore;
        }

        public int Count()
        {
            return dataStore.Count;
        }

        public IEnumerable<T> Where(Func<T, bool> predicate) 
        {
            return dataStore.Where(predicate);
        }

        public T Find(string name)
        {
            return dataStore.FirstOrDefault(x => x.Matches(name));
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

        public EntityGateway(IEnumerable<T> data)
        {
            dataStore = new List<T>();
            objectType = typeof(T);
            foreach(var obj in data)
            {
                dataStore.Add(obj);
            }
        }

        private void LoadObjects(IEnumerable<IObjectStore> data)
        {
            foreach(var d in data) {
                var typeInfo = typeof(T).GetTypeInfo();
                T obj;
                if(typeInfo.GetCustomAttribute<ObjectStoreSerializableAttribute>() != null)
                {
                    obj = d.Deserialize<T>(); 
                } else {
                    obj = objectType.Instantiate<T>(d);
                }
                dataStore.Add(obj);
            }
        }


    }

}