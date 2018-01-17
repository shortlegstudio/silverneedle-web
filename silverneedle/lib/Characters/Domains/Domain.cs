// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;

    public class Domain : LevelingClassFeature, IGatewayObject
    {
        public Domain(IObjectStore configure) : base(configure)
        {
            this.Spells = configure.GetList("spells");
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public override string ToString()
        {
            return string.Format("Domain ({0})", this.Name);
        }

        public string[] Spells { get; set; }

        public static Domain CreateForTesting(string name, IEnumerable<string> spells)
        {
            var memStore = new MemoryStore();
            memStore.SetValue("name", name);
            memStore.SetValue("spells", spells);
            return new Domain(memStore);
        }
    }
}