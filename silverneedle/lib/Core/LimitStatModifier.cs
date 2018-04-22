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
        private BasicStat maxValue;
        private AbilityScore abilityStat; 

        public LimitStatModifier(string statName, AbilityScore ability, BasicStat maxValue)
        {
            this.StatisticName = statName;
            this.abilityStat = ability;
            this.maxValue = maxValue;
        }
        public float Modifier 
        {
            get 
            { 
                return Math.Min(abilityStat.TotalModifier, maxValue.TotalValue);
            } 
        }

        public string ModifierType { get { return "Maximum"; } }

        public string StatisticName { get; private set; }
        public string Condition { get; set; }
        public string StatisticType { get; set; }
    }
}