// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterFeature : IComponent
    {
        private IList<CharacterFeatureAttribute> attributes = new List<CharacterFeatureAttribute>();
        protected CharacterFeature() 
        { 
            
        }
        public CharacterFeature(IObjectStore configuration)
        {
            LoadAttributes(configuration);
        }

        public IEnumerable<CharacterFeatureAttribute> Attributes
        {
            get
            {
                return this.attributes;
            }
        }

        public void Initialize(ComponentBag components)
        {
            foreach(var attr in Attributes)
            {
                components.Add(attr);
            }
        }

        private void LoadAttributes(IObjectStore configuration)
        {
            var configValues = configuration.GetObjectOptional("attributes");
            if(configValues == null)
                return;

            foreach(var attr in configValues.Children)
            {
               this.attributes.Add(
                   new CharacterFeatureAttribute(attr)
               );
            }
        }
    }
}