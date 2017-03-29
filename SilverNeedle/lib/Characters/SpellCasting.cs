// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Spells;

    public class SpellCasting
    {
        public const int MAX_SPELL_LEVEL = 9;
        private IDictionary<int, string[]> knownSpells;
        private IDictionary<int, string[]> preparedSpells;
        private int[] spellsPerDay;
        private Inventory inventory;
        
        public SpellsKnown SpellsKnown { get; set; }
        public int CasterLevel { get; set; }
        public int MaxLevel 
        { 
            get
            {
                return knownSpells.Keys.Max(); 
            }
        }
        public SpellCasting(Inventory inventory)
        {
            this.knownSpells = new Dictionary<int, string[]>();
            this.spellsPerDay = new int[MAX_SPELL_LEVEL];
            this.preparedSpells = new Dictionary<int, string[]>();
            this.inventory = inventory;
            SpellsKnown = SpellsKnown.None;
        }
        public string[] GetAvailableSpells(int level)
        {
            if(SpellsKnown == SpellsKnown.Spellbook)
            {
                var spellbook = inventory.Spellbooks.First();
                return spellbook.GetSpells(level);
            }
            return knownSpells[level];
        }

        public void AddSpells(int level, string[] list)
        {
            knownSpells[level] = list;
        }

        public void SetSpellsPerDay(int level, int amount)
        {
            spellsPerDay[level] = amount;
        }

        public int GetSpellsPerDay(int level)
        {
            return spellsPerDay[level];
        }

        public void PrepareSpells(int level, string[] spells)
        {
            preparedSpells[level] = spells;
        }

        public string[] GetPreparedSpells(int level)
        {
            if(!preparedSpells.ContainsKey(level))
                return new string[] { };
            return preparedSpells[level];
        }
    }
}