// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;

    public class Domain : IGatewayObject
    {
        public string Name { get; set; }

        private Domain() { }

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

        public static Domain CreateForTesting(string name, IEnumerable<string> spells)
        {
            var d = new Domain();
            d.Name = name;
            d.Spells = spells.ToArray();
            return d;
        }
    }
}