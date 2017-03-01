// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Utility;

    public class Occupation
    {
        public string Name { get; private set; }
        public string Class { get; private set; }

        public Occupation(IObjectStore data)
        {
            Name = data.GetString("name");
            Class = data.GetString("class");
        }
    }
}