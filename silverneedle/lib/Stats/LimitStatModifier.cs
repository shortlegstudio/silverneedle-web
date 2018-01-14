// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using System;
    using SilverNeedle.Characters;
    public class LimitStatModifier : IValueStatModifier
    {
        private BasicStat baseStat;
        private BasicStat maxValue;
        private AbilityScore abilityStat; 

        public LimitStatModifier(BasicStat baseStat, BasicStat maxValue)
        {
            this.baseStat = baseStat;
            this.maxValue = maxValue;
        }

        public LimitStatModifier(AbilityScore ability, BasicStat maxValue)
        {
            this.abilityStat = ability;
            this.maxValue = maxValue;
        }
        public float Modifier 
        {
            get 
            { 
                if(abilityStat != null)
                    return Math.Min(abilityStat.TotalModifier, maxValue.TotalValue);

                return Math.Min(baseStat.TotalValue, maxValue.TotalValue); 
            } 
        }

        public string Reason { get { return maxValue.Name; } }

        public string ModifierType { get { return "Maximum"; } }

        public string StatisticName { get { return baseStat.Name; } }
        public string Condition { get; set; }
        public string StatisticType { get; set; }
    }
}