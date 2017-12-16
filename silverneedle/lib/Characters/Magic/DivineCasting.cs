// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    public class DivineCasting : SpellCasting, ICastingPreparation
    {
        private Dictionary<int, IList<string>> readySpells = new Dictionary<int, IList<string>>();
        public DivineCasting(IObjectStore configuration) : base(configuration)
        {
        }

        public DivineCasting(IObjectStore configuration, EntityGateway<SpellList> spellLists) : base(configuration, spellLists)
        {
        }

        public override IEnumerable<string> GetKnownSpells(int spellLevel)
        {
            return SpellList.GetSpells(spellLevel, this.components.GetAll<ISpellCastingRule>());
        }

        public void PrepareSpell(int level, string spell)
        {
            if(!readySpells.ContainsKey(level))
                readySpells[level] = new List<string>();
            
            readySpells[level].Add(spell);
        }

        public void PrepareSpells(int level, IEnumerable<string> spells)
        {
            foreach(var spell in spells)
                PrepareSpell(level, spell);
        }

        public override IEnumerable<string> GetReadySpells(int level)
        {
            if(!readySpells.ContainsKey(level))
                return new string[] { };
            return readySpells[level];
        }
    }
}