// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Characters.Magic;

    public class IsSpellCaster : IPrerequisite
    {
        public bool IsQualified(CharacterSheet character)
        {
            return character.Contains<ISpellCasting>();
        }
    }
}