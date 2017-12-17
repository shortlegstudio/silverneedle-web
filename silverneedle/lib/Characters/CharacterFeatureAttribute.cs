// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    public class CharacterFeatureAttribute
    {
        public string TypeName { get; private set; }
        public IObjectStore Configuration { get; private set; }
        public CharacterFeatureAttribute(IObjectStore configuration)
        {
            this.TypeName = configuration.GetString("type");
            this.Configuration = configuration;
        }
    }
}