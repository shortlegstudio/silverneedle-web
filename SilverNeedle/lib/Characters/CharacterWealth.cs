// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Treasure;
    using SilverNeedle.Serialization;

    public class CharacterWealth : IGatewayObject
    {
        public string Name { get; set; }
        public IList<CharacterWealthLevel> Levels { get; private set; }

        public CharacterWealth()
        {
            Levels = new List<CharacterWealthLevel>();
        }
        public CharacterWealth(IObjectStore data) : this()
        {
            Name = data.GetString("name");
            var levels = data.GetObject("levels");
            foreach(var c in levels.Children)
            {
                var wl = new CharacterWealthLevel();
                wl.Level = c.GetInteger("level");
                wl.Value = c.GetString("amount").ToCoinValue();
                Levels.Add(wl);
            }
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        public class CharacterWealthLevel
        {
            public int Value { get; set; }
            public int Level { get; set; }

            public CharacterWealthLevel() { }
            public CharacterWealthLevel(int level, int val)
            {
                Level = level;
                Value = val;
            }
        }
    }
}