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
    public class EntityGateway<T> where T : IGatewayObject
    {
        private IList<T> dataStore;
        private Type entityType;
        private bool returnCopies = false;
        private bool isDeserializable = false;
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
            entityType = typeof(T);
            returnCopies = entityType.GetInterfaces().Contains(typeof(IGatewayCopy<T>));
            var typeInfo = entityType.GetTypeInfo();
            isDeserializable = typeInfo.GetCustomAttribute<ObjectStoreSerializableAttribute>() != null;
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
            var files = DatafileLoader.Instance.GetDataFiles<T>();
            return new EntityGateway<T>(files);
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

        public static EntityGateway<T> Empty()
        {
            return new EntityGateway<T>();
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
                if(d.HasKey("loader"))
                {
                    dataStore.Add(CreateFromLoader(d));
                }
                else
                {
                    dataStore.Add(CreateStandard(d));
                }
            }
        }

        private IEnumerable<T> CreateFromLoader(IObjectStore d)
        {
            var loader = d.GetString("loader").Instantiate<IGatewayLoader<T>>();
            return loader.Load(d);
        }

        private T CreateStandard(IObjectStore d)
        {
            T obj = InstantiateObject(d);
                
            if(isDeserializable)
            {
                d.Deserialize<T>(obj); 
            }
            return obj;
        }

        private T InstantiateObject(IObjectStore objectInformation)
        {
            if(objectInformation.HasKey("custom-implementation"))
            {
                var typename = objectInformation.GetString("custom-implementation");
                return typename.Instantiate<T>(objectInformation);
            } else {
                return entityType.Instantiate<T>(objectInformation);
            }
        }
    }
}