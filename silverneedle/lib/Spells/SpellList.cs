// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Spells
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;

    public class SpellList : IGatewayObject
    {
        public SpellList()
        {
            Levels = new Dictionary<int, IList<string>>();
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

        public void Add(int level, string spellName)
        {
            AddLevelIfMissing(level);
            if(!Levels[level].Contains(spellName))
                Levels[level].Add(spellName);
        }

        public IEnumerable<KeyValuePair<int, IList<string>>> FilterByMaxLevel(int maxLevel)
        {
            return this.Levels.Where(x => x.Key <= maxLevel);
        }

        private void AddLevelIfMissing(int level)
        {
            if(!Levels.ContainsKey(level))
            {
                Levels.Add(level, new List<string>());
            }
        }

        public string Class { get; set; }

        //TODO: Refactor this to be abstracted away
        public Dictionary<int, IList<string>> Levels { get; set; }

        public bool Matches(string cls)
        {
            return Class.EqualsIgnoreCase(cls);
        }
    }
}