// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ComponentBag
    {
        public event EventHandler<ComponentBagEvent> Added;
        public IList<object> All { get { return this.components; } }

        private List<object> components;

        public ComponentBag()
        {
            components = new List<object>();
        }

        public void Add(object obj)
        {
            var unique = obj as IEnsureUniqueComponent;
            if(unique != null)
            {
                if(components.Contains(unique))
                    return;
            }
            components.Add(obj);

            foreach(var notify in components.OfType<IComponent>())
            {
                OnComponentAdded(obj);
            } 
            InitializeComponent(obj);

            if(obj is IStatModifier)
            {
                ApplyStatModifier((IStatModifier)obj);
            }
        }

        public void Add(params object[] objects)
        {
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
            var stat = FindStat(statModifier.StatisticName);
            if(stat != null)
            {
                stat.AddModifier(statModifier);
            } else {
                ShortLog.ErrorFormat("Could Not Apply Modifier to Statistic: {0}", statModifier.StatisticName);
            }
        }

        public IEnumerable<IStatistic> GetAllStats()
        {
            return GetAll<IStatTracker>().SelectMany(x => x.Statistics);
        }

        public IStatistic FindStat(string name)
        {
            return GetAllStats().FirstOrDefault(x => x.Name.EqualsIgnoreCase(name));
        }

        public T FindStat<T>(string name)
        {
            return GetAllStats().Where(x => x.Name.EqualsIgnoreCase(name)).OfType<T>().FirstOrDefault();
        }

        private void OnComponentAdded(object component)
        {
            var args = new ComponentBagEvent();
            args.Component = component;
            if(this.Added != null)
            {
                this.Added(this, args);
            }
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

    public class ComponentBagEvent
    {
        public object Component; 
    }
}