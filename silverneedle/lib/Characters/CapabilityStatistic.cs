// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CapabilityStatistic : BasicStat, IAbility
    {
        public CapabilityStatistic(IObjectStore configuration) : base(configuration)
        {

        }

        public virtual string DisplayString()
        {
            return "{0} {1}".Formatted(this.Name, this.TotalValue.ToModifierString());
        }
    }
}