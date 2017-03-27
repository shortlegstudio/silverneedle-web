// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System.Collections.Generic;
    public class Spellbook : Gear
    {
        //TODO: Hardcoded prices and weight are probably bad... 
        public Spellbook() : base("Spellbook", 1500, 3) 
        {
            Spells = new Dictionary<int, string[]>();
            GroupSimilar = false;
        }


        public IDictionary<int, string[]> Spells { get; set; }
    }
}