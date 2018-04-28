// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Feature : IComponent, IFeature
    {
        [ObjectStore("name")]
        public virtual string Name { get; set; }
        public IEnumerable<IObjectStore> Items { get; private set; }
        public IEnumerable<IObjectStore> Commands { get; private set; }
        public IEnumerable<IObjectStore> Attributes { get; private set; }
        public ComponentContainer Parent { get; set; }
        public Feature(IObjectStore configuration)
        {
            configuration.Deserialize(this);
            this.Attributes = configuration.GetObjectListOptional("attributes").Default(new List<IObjectStore>());
            this.Items = configuration.GetObjectListOptional("items").Default(new List<IObjectStore>());
            this.Commands = configuration.GetObjectListOptional("commands").Default(new List<IObjectStore>());
        }

        public Feature() 
        {
            this.Attributes = new List<IObjectStore>();
            this.Items = new List<IObjectStore>();
            this.Commands = new List<IObjectStore>();
        }

        protected Feature(Feature copy)
        {
            this.Attributes = copy.Attributes;
            this.Name = copy.Name;
            this.Items = copy.Items;
            this.Commands = copy.Commands;
        }

        public virtual void Initialize(ComponentContainer components)
        {
            foreach(var attr in this.Attributes)
            {
                var typename = attr.GetString("attribute");

                if (string.IsNullOrEmpty(typename))
                    typename = "SilverNeedle.Feature";

                var instance = typename.Instantiate<object>(attr);
                components.Add(instance);
            }

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