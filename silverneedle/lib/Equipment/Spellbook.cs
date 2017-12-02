// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Utility;
    public class Spellbook : Gear
    {
        private IDictionary<int, IList<string>> Spells { get; set; }
        //TODO: Hardcoded prices and weight are probably bad... 
        public Spellbook() : base("Spellbook", 1500, 3) 
        {
            Spells = new Dictionary<int, IList<string>>();
            GroupSimilar = false;
        }

        public void AddSpells(int level, IEnumerable<string> spells)
        {
            GetLevelList(level).Add(spells);
        }

        public IEnumerable<string> GetSpells(int level)
        {
            return GetLevelList(level);
        }

        private IList<string> GetLevelList(int level)
        {

            if(!Spells.ContainsKey(level))
            {
                Spells.Add(level, new List<string>());
            }
            return Spells[level];

        }

        public bool ContainsSpell(int level, string name)
        {
            return GetSpells(level).Any(x => x.EqualsIgnoreCase(name));
        }
    }
}