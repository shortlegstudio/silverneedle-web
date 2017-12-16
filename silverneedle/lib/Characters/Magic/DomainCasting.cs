// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class DomainCasting : ISpellCasting, IComponent, ICastingPreparation
    {
        private ClassLevel sourceClass;
        private IEnumerable<Domain> domains;
        private Dictionary<int, IList<string>> readySpells = new Dictionary<int, IList<string>>();
        private AbilityScoreTypes castingAbilityType;
        public DomainCasting(IObjectStore configuration)
        {
            castingAbilityType = configuration.GetEnum<AbilityScoreTypes>("casting-ability");
        }

        public string SpellListName { get { return "Domain ({0})".Formatted(string.Join(", ", domains.Select(x => x.Name))); } }

        public SpellList SpellList { get; private set; }

        public int CasterLevel { get { return sourceClass.Level; } }

        public SpellType SpellType { get { return SpellType.Divine; } }

        public AbilityScore CastingAbility { get; private set; }
        
        public int GetDifficultyClass(int spellLevel)
        {
            return 10 + spellLevel + CastingAbility.TotalModifier;
        }

        public int GetHighestSpellLevelKnown()
        {
            return ((CasterLevel + 1) / 2).AtMost(9);
        }

        public IEnumerable<string> GetKnownSpells(int spellLevel)
        {
            return SpellList.GetSpells(spellLevel);
        }

        public IEnumerable<string> GetReadySpells(int spellLevel)
        {
            if(!readySpells.ContainsKey(spellLevel))
                return new string[] { };
            return readySpells[spellLevel];
        }

        public int GetSpellsPerDay(int level)
        {
            if(level == 0)
                return 0;
            if(level <= GetHighestSpellLevelKnown())
                return 1;

            return 0;
        }

        public bool HasSpells(int spellLevel)
        {
            return GetSpellsPerDay(spellLevel) > 0;
        }

        public void Initialize(ComponentBag components)
        {
            sourceClass = components.Get<ClassLevel>();
            domains = components.GetAll<Domain>();
            CastingAbility = components.Get<AbilityScores>().GetAbility(castingAbilityType);
            SpellList = new SpellList();
            foreach(var d in domains)
            {
                for(int spellLevel = 0; spellLevel < d.Spells.Length; spellLevel++)
                {
                    SpellList.Add(spellLevel + 1, d.Spells[spellLevel]);
                }
            }
        }

        public void PrepareSpell(int level, string spell)
        {
            if(!readySpells.ContainsKey(level))
                readySpells.Add(level, new List<string>());
            
            readySpells[level].Add(spell);
        }

        public void PrepareSpells(int level, IEnumerable<string> spells)
        {
            foreach(var s in spells)
                PrepareSpell(level, s);
        }
    }
}