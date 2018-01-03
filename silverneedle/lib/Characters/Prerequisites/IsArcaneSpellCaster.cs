// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;
    using System.Linq;

    public class IsArcaneSpellCaster : IPrerequisite
    {
        public bool IsQualified(ComponentContainer components)
        {
            var sc = components.GetAll<ISpellCasting>();
            return sc.Any(x => x.SpellType == SpellType.Arcane);
        }
    }
}