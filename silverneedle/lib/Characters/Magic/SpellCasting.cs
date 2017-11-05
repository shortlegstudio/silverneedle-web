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

    public class SpellCasting : ISpellCasting, IComponent
    {
        private AbilityScoreTypes castingAbilityType;
        private Dictionary<int, int[]> spellSlots = new Dictionary<int, int[]>();
        public SpellCasting(IObjectStore configuration) : this(configuration, GatewayProvider.Get<SpellList>())
        {

        }
        public SpellCasting(IObjectStore configuration, EntityGateway<SpellList> spellLists)
        {
            this.SpellListName = configuration.GetString("list");
            this.SpellList = spellLists.Find(this.SpellListName);
            this.SpellType = configuration.GetEnum<SpellType>("type");
            this.castingAbilityType = configuration.GetEnum<AbilityScoreTypes>("casting-ability");
            var slots = configuration.GetObject("spell-slots");
            foreach(var slot in slots.Keys)
            {
                var spellCounts = slots.GetList(slot).Select(x => x.ToInteger()).ToArray();
                spellSlots.Add(slot.ToInteger(), spellCounts);
            }

        }
        public virtual AbilityScore CastingAbility { get; private set; }

        public virtual IEnumerable<string> GetKnownSpells(int level)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<string> GetPreparedSpells(int level)
        {
            throw new System.NotImplementedException();
        }

        public virtual int GetSpellsPerDay(int level)
        {
            return spellSlots[CasterLevel][level] + GetBonusSpellsPerDay(level);
        }

        public virtual void AddSpells(int level, IEnumerable<Spell> list)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SetSpellsPerDay(int level, int amount)
        {
            throw new System.NotImplementedException();
        }

        public  virtual void PrepareSpells(int level, IEnumerable<string> listOfSpellNames)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Initialize(ComponentBag components)
        {
            this.CastingAbility = components.Get<AbilityScores>().GetAbility(castingAbilityType);
            this.Class = components.Get<ClassLevel>();
        }

        public virtual SpellsKnown SpellsKnown
        {
            get { return SpellsKnown.None; }
        }

        public virtual int MaxLevel => throw new System.NotImplementedException();

        public virtual string SpellListName { get; private set; }

        public virtual ClassLevel Class { get; private set; }

        public virtual int CasterLevel { get { return this.Class.Level; } }
        public virtual SpellType SpellType { get; private set; }
        public SpellList SpellList { get; private set; }

        //TODO: This is a general calculation that should be moved to a central
        //location for all spellcasters
        protected int GetBonusSpellsPerDay(int spellLevel)
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