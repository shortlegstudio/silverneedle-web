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
        int CasterLevel { get; }
        SpellType SpellType { get; }
        int GetSpellsPerDay(int level);
    }
}