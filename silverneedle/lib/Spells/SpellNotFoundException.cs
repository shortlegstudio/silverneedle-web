// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Spells
{
    public class SpellNotFoundException : System.Exception
    {
        public SpellNotFoundException(string spellName) : base("Count not location spell: {0}".Formatted(spellName)) { }
    }
}