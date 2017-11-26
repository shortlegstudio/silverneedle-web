// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    public class LearnSpellToken
    {
        public string SpellList { get; private set; }
        protected LearnSpellToken(string spellListName)
        {
            this.SpellList = spellListName;
        }

        public static LearnSpellToken FromList(string name)
        {
            return new LearnSpellToken(name);
        }
    }
}