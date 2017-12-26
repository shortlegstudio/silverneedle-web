// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;

    public class DamageReduction : BasicStat, IAbility
    {
        public DamageReduction(int amount) : this("-", amount) { }
        public DamageReduction(string bypassType, int baseAmount) : base(string.Format("DR({0})", bypassType), baseAmount)
        {
            this.BypassType = bypassType;
        }

        public DamageReduction(IObjectStore configuration) : base(configuration)
        {
            this.BypassType = configuration.GetString("bypass-type");
        }

        [ObjectStore("bypass-type")]
        public string BypassType { get; private set; }

        public string DisplayString()
        {
            return "{0}/{1}".Formatted(TotalValue, BypassType);
        }
    }

}