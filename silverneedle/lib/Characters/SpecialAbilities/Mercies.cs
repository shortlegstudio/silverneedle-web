// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using System.Linq;

    public class Mercies : IAbility
    {
        public Mercies() 
        {
            MercyList = new List<Mercy>();
        }

        public IList<Mercy> MercyList { get; set; }
        public void Add(Mercy mercy)
        {
            MercyList.Add(mercy);
        }

        public string DisplayString()
        {
            return string.Format("Mercies ({0})", string.Join(",", MercyList.Select(x => x.Name)));
        }
    }
}