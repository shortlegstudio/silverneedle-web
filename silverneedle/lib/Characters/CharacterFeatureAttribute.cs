// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterFeatureAttribute : IComponent
    {
        public string Name { get; private set; }
        private IObjectStore Items { get; set; }
        public IObjectStore Configuration { get; private set; }
        public CharacterFeatureAttribute(IObjectStore configuration)
        {
            this.Name = configuration.GetString("attribute");
            this.Items = configuration.GetObject("items");
        }

        public void Initialize(ComponentBag components)
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