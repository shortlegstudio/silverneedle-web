// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Spells
{
    using System.Collections.Generic;
    using SilverNeedle.Utility;

    public class SpellList
    {


        public SpellList(IObjectStore data) 
        {
            Levels = new Dictionary<int, string[]>();
            Class = data.GetString("class");

            var levelData = data.GetObject("levels");
            foreach(var key in levelData.Keys)
            {
                var level = int.Parse(key);
                var spells = levelData.GetList(key);
                Levels.Add(level, spells);
            }
        }

        public string Class { get; set; }

        public Dictionary<int, string[]> Levels { get; set; }

    }
}