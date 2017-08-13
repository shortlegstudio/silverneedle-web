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

        protected EntityGateway(T singleItem) : this()
        {
            dataStore.Add(singleItem);
        }

        protected EntityGateway(IEnumerable<IObjectStore> data) : this()
        {
            LoadObjects(data);
        }

        protected EntityGateway() 
        {
            dataStore = new List<T>();
            objectType = typeof(T);
        }

        protected EntityGateway(IEnumerable<T> data) : this()
        {
            foreach(var obj in data)
            {
                dataStore.Add(obj);
            }
        }

        public static EntityGateway<T> LoadFromDataFiles()
        {
            var gateway = new EntityGateway<T>();
            var files = DatafileLoader.Instance.GetDataFiles<T>();
            foreach(var f in files)
            {
                //NOTE: Root of a data file is the file itself not the objects contained
                //Kind of a tricky distinction, probably need a different data type
                //in the future
                gateway.LoadObjects(f.Children);
            }
            return gateway;
        }

        public static EntityGateway<T> LoadFromList(IEnumerable<T> initialItems)
        {
            return new EntityGateway<T>(initialItems);
        }

        public static EntityGateway<T> LoadFromObjectStore(IEnumerable<IObjectStore> data)
        {
            return new EntityGateway<T>(data);
        }

        public static EntityGateway<T> LoadWithSingleItem(T item)
        {
            return new EntityGateway<T>(item);
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
            var type = typeof(T);
            var typeInfo = type.GetTypeInfo();
            returnCopies = type.GetInterfaces().Contains(typeof(IGatewayCopy<T>));

            foreach(var d in data) {
                T obj;
                if(d.HasKey("custom-implementation"))
                {
                    var typename = d.GetString("custom-implementation");
                    obj = typename.Instantiate<T>(d);
                } else {
                    obj = objectType.Instantiate<T>(d);
                }

                if(typeInfo.GetCustomAttribute<ObjectStoreSerializableAttribute>() != null)
                {
                    d.Deserialize<T>(obj); 
                }
                dataStore.Add(obj);
            }
        }
    }
}