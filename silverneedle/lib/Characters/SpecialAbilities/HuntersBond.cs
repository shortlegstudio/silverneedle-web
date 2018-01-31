// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class HuntersBond : IAbility
    {
        public HuntersBond(string bond) : base()
        {
            this.Bond = bond;
        }

        public string Bond { get; set; }

        public string DisplayString()
        {
            return string.Format("Hunters Bond ({0})", this.Bond);
        }
    }
}