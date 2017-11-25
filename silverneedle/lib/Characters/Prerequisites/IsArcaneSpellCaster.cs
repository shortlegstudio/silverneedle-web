// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;
    using System.Linq;

    public class IsArcaneSpellCaster : IPrerequisite
    {
        public bool IsQualified(CharacterSheet character)
        {
            var sc = character.GetAll<ISpellCasting>();
            return sc.Any(x => x.SpellType == SpellType.Arcane);
        }
    }
}