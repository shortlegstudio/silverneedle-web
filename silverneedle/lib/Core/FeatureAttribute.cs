// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class FeatureAttribute : IComponent, IFeatureAttribute
    {
        public virtual string Name { get; protected set; }
        private IEnumerable<IObjectStore> Items { get; set; }
        private IEnumerable<IObjectStore> Commands { get; set; }
        public FeatureAttribute(IObjectStore configuration)
        {
            this.Name = configuration.GetString("name");
            this.Items = configuration.GetObjectListOptional("items").Default(new List<IObjectStore>());
            this.Commands = configuration.GetObjectListOptional("commands").Default(new List<IObjectStore>());
        }

        protected FeatureAttribute(FeatureAttribute copy)
        {
            this.Name = copy.Name;
            this.Items = copy.Items;
            this.Commands = copy.Commands;
        }

        public virtual void Initialize(ComponentContainer components)
        {
            foreach(var item in this.Items)
            {
                var typename = item.GetString("type");
                var instance = typename.Instantiate<object>(item);
                components.Add(instance);
            }

            foreach(var command in this.Commands)
            {
                var typename = command.GetString("command");
                var instance = typename.Instantiate<IFeatureCommand>(command);
                instance.Execute(components);
            }
        }
    }
}