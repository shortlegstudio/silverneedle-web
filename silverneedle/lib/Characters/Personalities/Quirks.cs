// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Personalities
{
    using System.Collections.Generic;
    public class Quirks
    {
        public IList<string> Items { get; private set; }

        public Quirks()
        {
            this.Items = new List<string>();
        }
    }
}