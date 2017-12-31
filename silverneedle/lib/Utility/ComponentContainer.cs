// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ComponentContainer
    {
        public IList<object> All { get { return this.components; } }

        private List<object> components;

        public ComponentContainer()
        {
            components = new List<object>();
        }

        public void Add(object obj)
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

            if(obj is IStatModifier)
            {
                ApplyStatModifier((IStatModifier)obj);
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

        public void ApplyStatModifiers(IEnumerable<IStatModifier> statModifiers)
        {
            foreach(var mod in statModifiers)
            {
                ApplyStatModifier(mod);
            }
        }

        public void ApplyStatModifier(IStatModifier statModifier)
        {
            var stats = FindMatchingStats(statModifier.StatisticName);

            if(stats.Empty())
            {
                ShortLog.ErrorFormat("Could Not Apply Modifier to Statistic: {0}", statModifier.StatisticName);
            }

            foreach(var stat in stats)
            {
                stat.AddModifier(statModifier);
            }
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
            return GetAllStats().Where(x => x.Matches(name));
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