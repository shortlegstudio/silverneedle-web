// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterFeatureAttribute : IComponent, ICharacterFeatureAttribute
    {
        public virtual string Name { get; protected set; }
        private IObjectStore Items { get; set; }
        private IObjectStore Commands { get; set; }
        public CharacterFeatureAttribute(IObjectStore configuration)
        {
            this.Name = configuration.GetString("name");
            this.Items = configuration.GetObjectOptional("items").Default(new MemoryStore());
            this.Commands = configuration.GetObjectOptional("commands").Default(new MemoryStore());
        }

        protected CharacterFeatureAttribute(CharacterFeatureAttribute copy)
        {
            this.Name = copy.Name;
            this.Items = copy.Items;
            this.Commands = copy.Commands;
        }

        public virtual void Initialize(ComponentContainer components)
        {
            foreach(var item in this.Items.Children)
            {
                var typename = item.GetString("type");
                var instance = typename.Instantiate<object>(item);
                components.Add(instance);
            }

            foreach(var command in this.Commands.Children)
            {
                var typename = command.GetString("command");
                var instance = typename.Instantiate<ICharacterFeatureCommand>(command);
                instance.Execute(components);
            }
        }
    }
}