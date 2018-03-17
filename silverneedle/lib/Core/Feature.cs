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
        private IList<FeatureAttribute> attributes = new List<FeatureAttribute>();
        protected Feature() 
        { 
            
        }
        public Feature(IObjectStore configuration)
        {
            LoadAttributes(configuration);
        }

        public IEnumerable<IFeatureAttribute> Attributes
        {
            get
            {
                return this.attributes;
            }
        }

        public void Initialize(ComponentContainer components)
        {
            foreach(var attr in Attributes)
            {
                components.Add(attr);
            }
        }

        private void LoadAttributes(IObjectStore configuration)
        {
            var configValues = configuration.GetObjectListOptional("attributes");
            if(configValues == null)
                return;

            foreach(var attr in configValues)
            {
                var attributeType = attr.GetString("attribute");
                if(string.IsNullOrEmpty(attributeType))
                    attributeType = typeof(FeatureAttribute).FullName;

                this.attributes.Add(
                    attributeType.Instantiate<FeatureAttribute>(attr)
                );
            }
        }
    }
}