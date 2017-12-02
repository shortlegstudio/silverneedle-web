// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    public interface ICastingPerparation
    {
        int GetSpellsPerDay(int level);
        void PrepareSpell(int level, string spell);
        void PrepareSpells(int level, IEnumerable<string> spells);
        IEnumerable<string> GetKnownSpells(int level);
    }
}