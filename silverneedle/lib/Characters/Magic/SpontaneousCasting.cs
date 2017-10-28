// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;

    public class SpontaneousCasting : ISpellCasting
    {
        public SpontaneousCasting(IObjectStore configuration)
        {

        }
        public AbilityScore CastingAbility => throw new System.NotImplementedException();

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
            throw new System.NotImplementedException();
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

        public SpellsKnown SpellsKnown
        {
            get { return SpellsKnown.Spontaneous; }
        }

        public int MaxLevel => throw new System.NotImplementedException();

        public string SpellList => throw new System.NotImplementedException();

        public ClassLevel Class => throw new System.NotImplementedException();

        public int CasterLevel => throw new System.NotImplementedException();
    }
}