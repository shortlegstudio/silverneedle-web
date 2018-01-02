// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterStrategyValue : IComponent
    {
        private IObjectStore values;
        public CharacterStrategyValue(IObjectStore configuration)
        {
            this.Name = configuration.GetString("name");
            this.values = configuration.GetObject("values");
        }

        public void Initialize(ComponentContainer components)
        {
            var strat = components.Get<CharacterStrategy>();
            foreach(var item in values.Children)
            {
                strat.AddCustomValue(this.Name, item.GetString("name"), item.GetInteger("weight"));
            }
        }

        public string Name { get; private set; }
    }
}