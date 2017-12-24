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
        public virtual string Name { get; private set; }
        private IObjectStore Items { get; set; }
        public CharacterFeatureAttribute(IObjectStore configuration)
        {
            this.Name = configuration.GetString("name");
            this.Items = configuration.GetObject("items");
        }

        public void Initialize(ComponentContainer components)
        {
            foreach(var item in this.Items.Children)
            {
                var typename = item.GetString("type");
                var instance = typename.Instantiate<object>(item);
                components.Add(instance);
            }
        }
    }
}