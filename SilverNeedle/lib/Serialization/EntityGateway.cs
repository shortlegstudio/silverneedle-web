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
        private bool returnCopies = false;
        public IEnumerable<T> All()
        {
            return GetDataStore();
        }

        public int Count()
        {
            return dataStore.Count;
        }

        public IEnumerable<T> Where(Func<T, bool> predicate) 
        {
            return GetDataStore().Where(predicate);
        }

        public T FindOrNull(string name)
        {
            return GetDataStore().FirstOrDefault(x => x.Matches(name));
        }

        public T Find(string name)
        {
            var entry = GetDataStore().FirstOrDefault(x => x.Matches(name));
            if(entry == null)
                throw new EntityNotFoundException(string.Format("Could not find '{0}' in types of {1}.", name, typeof(T).Name));

            return entry;
        }

        public IEnumerable<T> FindAll(IEnumerable<string> names)
        {
            return names.Select(x => Find(x));
        }

        public T ChooseOne()
        {
            return GetDataStore().ChooseOne();
        }

        public IEnumerable<T> Choose(int number)
        {
            return GetDataStore().Choose(number);
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

        private IList<T> GetDataStore()
        {
            if(returnCopies)
            {
                return dataStore.Cast<IGatewayCopy<T>>().Select(x => x.Copy()).ToList();
            }

            return dataStore;
        }

        private void LoadObjects(IEnumerable<IObjectStore> data)
        {
            foreach(var d in data) {
                var typeInfo = typeof(T).GetTypeInfo();
                returnCopies = typeInfo.GetInterfaces().Contains(typeof(IGatewayCopy<T>));
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