// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System.Collections.Generic;
    public class Spellbook : Gear
    {
        //TODO: Hardcoded prices and weight are probably bad... 
        public Spellbook() : base("Spellbook", 1500, 3) 
        {
            Spells = new Dictionary<int, IList<string>>();
            GroupSimilar = false;
        }

        public void AddSpells(int level, IList<string> spells)
        {
            this.Spells[level] = spells;
        }

        public IList<string> GetSpells(int level)
        {
            return Spells[level];
        }

        private IDictionary<int, IList<string>> Spells { get; set; }
    }
}