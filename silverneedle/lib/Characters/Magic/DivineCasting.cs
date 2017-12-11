// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Spells;

    public class DivineCasting : ISpellCasting, ICastingPerparation
    {
        public const int MAX_SPELL_LEVEL = 10;
        private IDictionary<int, Spell[]> knownSpells;
        private IDictionary<int, IList<Spell>> preparedSpells;
        private int[] spellsPerDay;
        public SpellsKnown SpellsKnown { get; set; }
        public SpellList SpellList => throw new System.NotImplementedException();
        public ClassLevel Class { get; }
        public int CasterLevel { get { return this.Class.Level; } }
        public AbilityScore CastingAbility { get; private set; }

        public string SpellListName { get; private set; }
        private BasicStat DifficultyClass { get; set; }
        public SpellType SpellType { get; private set; }
        private IList<ISpellCastingRule> castingRules;
        public int GetHighestSpellLevelKnown()
        {
            return MaxLevel;
        }

        public IEnumerable<string> GetReadySpells(int spellLevel)
        {
            return GetPreparedSpells(spellLevel);
        }

        public IEnumerable<string> GetKnownSpells(int spellLevel)
        {
            return GetCastableSpells(spellLevel).Select(x => x.Name);
        }

        public virtual int MaxLevel 
        { 
            get
            {
                return knownSpells.Keys.Max(); 
            }
        }


        public DivineCasting(ClassLevel sourceClass, string spellList)
        {
            this.Class = sourceClass;
            this.knownSpells = new Dictionary<int, Spell[]>();
            this.spellsPerDay = new int[MAX_SPELL_LEVEL];
            this.preparedSpells = new Dictionary<int, IList<Spell>>();
            this.DifficultyClass = new BasicStat(StatNames.SpellcastingDC, 10);
            SpellsKnown = SpellsKnown.None;
            this.castingRules = new List<ISpellCastingRule>();
            this.SpellListName = spellList;
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

        public virtual IEnumerable<string> GetAvailableSpells(int level)
        {
            return GetCastableSpells(level).Select(spell => spell.Name);
        }

        public virtual void AddSpells(int level, IEnumerable<Spell> list)
        {
            ShortLog.DebugFormat("Adding Spells[{0}]: {1}", level.ToString(), list.ToString());
            if(knownSpells.ContainsKey(level))
            {
                knownSpells[level] = knownSpells[level].Concat(list).ToArray();
            }
            else
            {
                knownSpells[level] = list.ToArray();
            }
        }

        public virtual void SetSpellsPerDay(int level, int amount)
        {
            spellsPerDay[level] = amount;
        }

        public virtual int GetSpellsPerDay(int level)
        {
            if(level >= spellsPerDay.Length)
                return 0;
            return spellsPerDay[level];
        }

        public virtual void PrepareSpells(int level, IEnumerable<string> spells)
        {
            if(spells.Empty())
                return;
            foreach(var spell in spells)
            {
                PrepareSpell(level, spell);
            }
        } 

        public void PrepareSpell(int level, string spell)
        {
            if(!preparedSpells.ContainsKey(level))
                preparedSpells[level] = new List<Spell>();

            preparedSpells[level].Add(knownSpells[level].First(x => x.Name == spell));
        }

        public virtual IEnumerable<string> GetPreparedSpells(int level)
        {
            if(!preparedSpells.ContainsKey(level))
                return new string[] { };
            return preparedSpells[level].Select(x => x.Name);
        }

        public void AddRule(ISpellCastingRule rule)
        {
            this.castingRules.Add(rule);
        }

        private IEnumerable<Spell> GetCastableSpells(int level)
        {
            if(!knownSpells.ContainsKey(level))
                return new Spell[] { };
            return knownSpells[level].Where(spell => castingRules.All(rule => rule.CanCastSpell(spell)));
        }

        public override string ToString()
        {
            return string.Format("Spellcasting ({0})", this.SpellListName);
        }

        public bool HasSpells(int spellLevel)
        {
            return GetSpellsPerDay(spellLevel) > 0;
        }
    }
}