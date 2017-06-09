// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Spells;

    public class SpellCasting
    {
        public const int MAX_SPELL_LEVEL = 10;
        private IDictionary<int, Spell[]> knownSpells;
        private IDictionary<int, Spell[]> preparedSpells;
        private int[] spellsPerDay;
        private Inventory inventory;
        public SpellsKnown SpellsKnown { get; set; }
        public int CasterLevel { get; set; }
        public AbilityScore CastingAbility { get; private set; }
        private BasicStat DifficultyClass { get; set; }
        private IList<ISpellCastingRule> castingRules;

        public int MaxLevel 
        { 
            get
            {
                return knownSpells.Keys.Max(); 
            }
        }
        public SpellCasting(Inventory inventory)
        {
            this.knownSpells = new Dictionary<int, Spell[]>();
            this.spellsPerDay = new int[MAX_SPELL_LEVEL];
            this.preparedSpells = new Dictionary<int, Spell[]>();
            this.inventory = inventory;
            this.DifficultyClass = new BasicStat(StatNames.SpellcastingDC, 10);
            SpellsKnown = SpellsKnown.None;
            this.castingRules = new List<ISpellCastingRule>();
        }

        public void SetCastingAbility(AbilityScore ability)
        {
            CastingAbility = ability;
            this.DifficultyClass.AddModifier(new AbilityStatModifier(ability));
        }

        public int GetDifficultyClass(int spellLevel)
        {
            return DifficultyClass.TotalValue + spellLevel;
        }

        public string[] GetAvailableSpells(int level)
        {
            if(SpellsKnown == SpellsKnown.Spellbook)
            {
                var spellbook = inventory.Spellbooks.First();
                return spellbook.GetSpells(level);
            }
            return GetCastableSpells(level).Select(spell => spell.Name).ToArray();
        }

        public void AddSpells(int level, Spell[] list)
        {
            ShortLog.DebugFormat("Adding Spells[{0}]: {1}", level.ToString(), list.ToString());
            knownSpells[level] = list;
        }

        public void SetSpellsPerDay(int level, int amount)
        {
            spellsPerDay[level] = amount;
        }

        public int GetSpellsPerDay(int level)
        {
            if(level >= spellsPerDay.Length)
                return 0;
            return spellsPerDay[level];
        }

        public void PrepareSpells(int level, string[] spells)
        {
            preparedSpells[level] = knownSpells[level].Where(x => spells.Contains(x.Name)).ToArray();
        } 

        public string[] GetPreparedSpells(int level)
        {
            if(!preparedSpells.ContainsKey(level))
                return new string[] { };
            return preparedSpells[level].Select(x => x.Name).ToArray();
        }

        public void AddRule(ISpellCastingRule rule)
        {
            this.castingRules.Add(rule);
        }

        private IEnumerable<Spell> GetCastableSpells(int level)
        {
            return knownSpells[level].Where(spell => castingRules.All(rule => rule.CanCastSpell(spell)));
        }
    }
}