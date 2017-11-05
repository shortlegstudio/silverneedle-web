// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using SilverNeedle.Spells;

    public interface ISpellCasting
    {

        AbilityScore CastingAbility { get; }
        ClassLevel Class { get; }
        int MaxLevel { get; }
        int CasterLevel { get; }
        string SpellListName { get; }
        SpellsKnown SpellsKnown { get; }
        SpellType SpellType { get; }
        IEnumerable<string> GetAvailableSpells(int level);
        IEnumerable<string> GetPreparedSpells(int level);
        int GetSpellsPerDay(int level);
        void SetSpellsPerDay(int level, int amount);
        void AddSpells(int level, IEnumerable<Spell> list);
        void PrepareSpells(int level, IEnumerable<string> listOfSpellNames);
    }
}