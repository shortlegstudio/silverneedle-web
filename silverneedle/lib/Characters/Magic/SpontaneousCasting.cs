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

    public class SpontaneousCasting : ISpellCasting, IComponent
    {
        private AbilityScoreTypes castingAbilityType;
        private Dictionary<int, int[]> spellSlots = new Dictionary<int, int[]>();
        public SpontaneousCasting(IObjectStore configuration)
        {
            this.SpellList = configuration.GetString("list");
            this.SpellType = configuration.GetEnum<SpellType>("type");
            this.castingAbilityType = configuration.GetEnum<AbilityScoreTypes>("casting-ability");
            var slots = configuration.GetObject("spell-slots");
            foreach(var slot in slots.Keys)
            {
                var spellCounts = slots.GetList(slot).Select(x => x.ToInteger()).ToArray();
                spellSlots.Add(slot.ToInteger(), spellCounts);
            }

        }
        public AbilityScore CastingAbility { get; private set; }

        public IEnumerable<string> GetAvailableSpells(int level)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetPreparedSpells(int level)
        {
            throw new System.NotImplementedException();
        }

        public int GetSpellsPerDay(int level)
        {
            return spellSlots[CasterLevel][level] + GetBonusSpellsPerDay(level);
        }

        public void AddSpells(int level, IEnumerable<Spell> list)
        {
            throw new System.NotImplementedException();
        }

        public void SetSpellsPerDay(int level, int amount)
        {
            throw new System.NotImplementedException();
        }

        public void PrepareSpells(int level, IEnumerable<string> listOfSpellNames)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(ComponentBag components)
        {
            this.CastingAbility = components.Get<AbilityScores>().GetAbility(castingAbilityType);
            this.Class = components.Get<ClassLevel>();
        }

        public SpellsKnown SpellsKnown
        {
            get { return SpellsKnown.Spontaneous; }
        }

        public int MaxLevel => throw new System.NotImplementedException();

        public string SpellList { get; private set; }

        public ClassLevel Class { get; private set; }

        public int CasterLevel { get { return this.Class.Level; } }
        public SpellType SpellType { get; private set; }

        //TODO: This is a general calculation that should be moved to a central
        //location for all spellcasters
        private int GetBonusSpellsPerDay(int spellLevel)
        {
            if(spellLevel == 0)
                return 0;

            // Ability: 16 (+3) Spell Level: 3
            // (3-3 + 4)/4 = 1 
            // Ability: 20 (+5) Spell Level: 1
            // (5-1 + 4)/4 = 2 
            return (CastingAbility.TotalModifier - spellLevel + 4) / 4;
        }
    }
}