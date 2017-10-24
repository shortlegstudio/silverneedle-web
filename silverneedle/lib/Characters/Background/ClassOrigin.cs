// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    public class ClassOrigin
    {
        public ClassOrigin()
        {
            Traits = new List<string>();
            Storylines = new List<string>();
        }

        public ClassOrigin(string cls, IObjectStore data) : this()
        {
            Class = cls;
            Name = data.GetString("name");
            Weighting = data.GetInteger("weight");
            Traits.Add(data.GetListOptional("traits"));
            Storylines.Add(data.GetListOptional("storylines"));
        }

        public string Name { get; set; }
        public string Class { get; set; }
        public int Weighting { get; set; }
        public IList<string> Traits { get; set; }
        public IList<string> Storylines { get; set; }
    }
}

