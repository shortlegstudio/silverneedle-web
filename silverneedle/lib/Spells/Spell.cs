// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Spells
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;

    public class Spell : IGatewayObject
    {
        public Spell(IObjectStore data)
        {
            Name = data.GetString("name");
            School = data.GetString("school");
            Subschool = data.GetStringOptional("subschool");
            Descriptors = data.GetListOptional("descriptors");
        }

        public Spell(string spellName, string school) : this(spellName, school, new string[] { })
        {
        }

        public Spell(string spellName, string school, IEnumerable<string> descriptors)
        {
            this.Name = spellName;
            this.School = school;
            this.Descriptors = descriptors.ToArray();
        }

        public string Name { get; set; }
        public string School { get; set; }
        public string Subschool { get; set; }
        public string[] Descriptors { get; set; }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}