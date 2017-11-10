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

        string SpellListName { get; }
        int GetHighestSpellLevelKnown();
        IEnumerable<string> GetReadySpells(int spellLevel);
        int GetDifficultyClass(int spellLevel);
    }
}