// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class FavoredClassOption : IGatewayObject
    {
        private IObjectStore optionConfiguration;
        public FavoredClassOption(IObjectStore configuration)
        {
            optionConfiguration = configuration;
        }

        public IStatModifier CreateModifier()
        {
            var typeName = optionConfiguration.GetString("type");
            return typeName.Instantiate<IStatModifier>(optionConfiguration);
        }

        public bool Matches(string name)
        {
            return false;
        }
    }
}