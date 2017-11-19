// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class SpontaneousCasting : SpellCasting
    {
        private Dictionary<int, int[]> knownSpells = new Dictionary<int, int[]>();
        private Dictionary<int, List<string>> learnedSpells = new Dictionary<int, List<string>>();
        public SpontaneousCasting(IObjectStore configuration) : this(configuration, GatewayProvider.Get<SpellList>()) 
        {
        }

        public SpontaneousCasting(IObjectStore configuration, EntityGateway<SpellList> spellLists) : base(configuration, spellLists)
        {
            LoadKnownSpellList(configuration.GetObject("spells-known"));
        }

        private void LoadKnownSpellList(IObjectStore knownSpellConfiguration)
        {
            foreach(var levelKey in knownSpellConfiguration.Keys)
            {
                knownSpells[levelKey.ToInteger()] = knownSpellConfiguration.GetList(levelKey).Select(x => x.ToInteger()).ToArray();
            }
        }

        public int GetKnownSpellCount(int level)
        {
            if(level > GetHighestSpellLevelKnown())
                return 0;
            return knownSpells[this.CasterLevel][level];
        }

        public override int GetHighestSpellLevelKnown()
        {
            return knownSpells[this.CasterLevel].Length - 1;
        }

        public void LearnSpell(string spellName)
        {
            var spellLevel = SpellList.GetSpellLevel(spellName);
            LearnSpell(spellLevel, spellName);
        }

        public void LearnSpell(int spellLevel, string spellName)
        {
            if(!learnedSpells.ContainsKey(spellLevel))
            {
                learnedSpells.Add(spellLevel, new List<string>());
            }

            learnedSpells[spellLevel].Add(spellName);
        }

        public override IEnumerable<string> GetReadySpells(int spellLevel)
        {
            if(!learnedSpells.ContainsKey(spellLevel))
                return new string[] { };
            
            return learnedSpells[spellLevel];
        }

    }
}