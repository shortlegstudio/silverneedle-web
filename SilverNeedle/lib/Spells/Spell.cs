// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Spells
{
    using SilverNeedle.Serialization;

    public class Spell
    {
        public Spell(IObjectStore data)
        {
            Name = data.GetString("name");
            School = data.GetString("school");
        }

        public string Name { get; set; }
        public string School { get; set; }
    }
}