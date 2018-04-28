// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class KiPool : BasicStat, IAbility, IComponent
    {
        public ComponentContainer Parent { get; set; }

        public KiPool() : base("Ki Pool")
        {

        }
        public KiPool(IObjectStore configuration) : base(configuration)
        {
        }
        public void Initialize(ComponentContainer components) { }

        private IEnumerable<KiStrike> GetKiStrikes()
        {
            return Parent.GetAll<KiStrike>();
        }

        public string DisplayString()
        {
            return string.Format("Ki Pool ({0} points {1})",
                this.TotalValue,
                string.Join(", ", GetKiStrikes().Select(x => x.DamageType))
            );
        }
    }
}