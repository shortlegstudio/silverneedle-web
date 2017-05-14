// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class HuntersBond : SpecialAbility
    {
        public HuntersBond(string bond) : base()
        {
            this.Bond = bond;
        }

        public string Bond { get; set; }

        public override string Name
        {
            get
            {
                return string.Format("Hunters Bond ({0})", this.Bond);
            }

        }
    }
}