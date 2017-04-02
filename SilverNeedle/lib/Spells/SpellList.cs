// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Spells
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    public class SpellList : IGatewayObject
    {
        public SpellList()
        {
            Levels = new Dictionary<int, string[]>();
        }
        public SpellList(IObjectStore data) : this()
        {
            Class = data.GetString("class");

            var levelData = data.GetObject("levels");
            foreach(var key in levelData.Keys)
            {
                var level = int.Parse(key);
                var spells = levelData.GetList(key);
                Levels.Add(level, spells);
            }
        }

        public string Class { get; set; }

        public Dictionary<int, string[]> Levels { get; set; }

        public bool Matches(string cls)
        {
            return Class.EqualsIgnoreCase(cls);
        }
    }
}