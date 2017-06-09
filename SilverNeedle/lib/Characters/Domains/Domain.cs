// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using SilverNeedle.Serialization;

    public class Domain : IGatewayObject
    {
        public string Name { get; set; }

        public Domain(IObjectStore configure)
        {
            this.Name = configure.GetString("name");
            this.Spells = configure.GetList("spells");
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public override string ToString()
        {
            return string.Format("Domain ({0})", this.GetType().Name);
        }

        public string[] Spells { get; set; }
    }
}