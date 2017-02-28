// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Groups
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;

    public class Settlement
    {
        public Settlement()
        {
            inhabitants = new List<CharacterSheet>();
        }

        public int Population { get { return inhabitants.Count; } }
        public IEnumerable<CharacterSheet> Inhabitants { get { return inhabitants; } }
        
        public void AddInhabitant(CharacterSheet character)
        {
            inhabitants.Add(character);
        }

        private IList<CharacterSheet> inhabitants;

    }
}