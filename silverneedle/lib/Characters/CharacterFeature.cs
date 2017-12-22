// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterFeature : IComponent, ICharacterFeature
    {
        private IList<CharacterFeatureAttribute> attributes = new List<CharacterFeatureAttribute>();
        protected CharacterFeature() 
        { 
            
        }
        public CharacterFeature(IObjectStore configuration)
        {
            LoadAttributes(configuration);
        }

        public IEnumerable<ICharacterFeatureAttribute> Attributes
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
                var attributeType = attr.GetString("attribute");
                if(string.IsNullOrEmpty(attributeType))
                    attributeType = "SilverNeedle.Characters.CharacterFeatureAttribute";

                this.attributes.Add(
                    attributeType.Instantiate<CharacterFeatureAttribute>(attr)
                );
            }
        }
    }
}