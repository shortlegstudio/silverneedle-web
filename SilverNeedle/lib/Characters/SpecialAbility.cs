// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    public class SpecialAbility
    {
        public SpecialAbility()
        {
        }

        public SpecialAbility(string condition, string type)
        {
            this.Condition = condition;
            this.Type = type;
        }

        public string Condition { get; set; }
        public string Type { get; set; }

        public string Name { get; set; }
    }
}

