// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using SilverNeedle.Serialization;

    public class ComponentContainer
    {
        public IEnumerable<object> All 
        { 
            get { return this.components; } 
        }

        [ObjectStoreOptional("component-store")]
        public object[] BackingDataStore 
        { 
            get { return components.ToArray(); } 
            private set { components.AddRange(value); }
        }

        private List<object> components = new List<object>();

        public ComponentContainer(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public ComponentContainer() { }

        public void Add(object obj)
        {
            PerformAdd(obj);
        }

        private void PerformAdd(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException("obj");

            var unique = obj as IEnsureUniqueComponent;
            if(unique != null)
            {
                if(components.Contains(unique))
                    return;
            }
            components.Add(obj);
            InitializeComponent(obj);

            if(obj is IStatisticModifier)
            {
                ApplyStatModifier((IStatisticModifier)obj);
            }

            InspectObjectForCustomAttributes(obj);
        }

        private void InspectObjectForCustomAttributes(object obj)
        {
            var typeInfo = obj.GetType();
            var properties = typeInfo.GetProperties();
            foreach(var prop in properties)
            {
                var attribute = prop.GetCustomAttribute<AddToContainerAttribute>();
                if(attribute == null)
                    continue;

                Add(prop.GetValue(obj));
            }
        }

        public void Add(params object[] objects)
        {
            if(objects == null)
                throw new ArgumentNullException("objects");

            foreach(var o in objects)
                Add(o);
        }

        public void AddNoInitialize(object[] objects)
        {
            foreach(var o in objects)
                components.Add(o);
        }

        public void Remove<T>()
        {
            components.RemoveAll(x => x.GetType() == typeof(T));
        }

        public T Get<T>()
        {
            return components.OfType<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return components.OfType<T>();
        }

        public void Replace<T>(T value)
        {
            var old = Get<T>();
            components.Remove(old);
            Add(value);
        }

        public bool Contains<T>()
        {
            return Get<T>() != null;
        }

        public bool Contains(object obj)
        {
            return components.Contains(obj);
        }

        public void ApplyStatModifiers(IEnumerable<IStatisticModifier> statModifiers)
        {
            foreach(var mod in statModifiers)
            {
                ApplyStatModifier(mod);
            }
        }

        public void ApplyStatModifier(IStatisticModifier statModifier)
        {
            var stats = FindMatchingStats(statModifier.StatisticName);

            if(stats.Empty())
            {
                ShortLog.ErrorFormat("Could Not Apply Modifier to Statistic: {0}", statModifier.StatisticName);
            }

            foreach(var stat in stats)
            {
                if(ValidateStatType(stat, statModifier))
                    stat.AddModifier(statModifier);
            }
        }

        private bool ValidateStatType(IStatistic statistic, IStatisticModifier modifier)
        {
            if(string.IsNullOrEmpty(modifier.StatisticType))
                return true;
            var type = Reflector.FindType(modifier.StatisticType);
            return statistic.Implements(type);
        }

        public IEnumerable<IStatistic> GetAllStats()
        {
            return GetAll<IStatTracker>().SelectMany(x => x.Statistics)
                .Union(GetAll<IStatistic>());
        }

        public IStatistic FindStat(string name)
        {
            return GetAllStats().FirstOrDefault(x => x.Matches(name));
        }

        public IEnumerable<IStatistic> FindMatchingStats(string name)
        {
            return GetAllStats().Where(x => x.Name.SearchFor(name));
        }

        public T FindStat<T>(string name)
        {
            return GetAllStats().Where(x => x.Name.EqualsIgnoreCase(name)).OfType<T>().FirstOrDefault();
        }

        private void InitializeComponent(object obj)
        {
            var comp = obj as IComponent;
            if(comp != null)
            {
                comp.Initialize(this);
            }
        }
    }
}