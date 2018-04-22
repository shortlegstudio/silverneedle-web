// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;

namespace SilverNeedle
{
    public class DelegateStatModifier : IValueStatModifier
    {
        protected Func<float> Calculation;

        public float Modifier { get { return Calculation(); } }

        public string ModifierType { get; private set; }

        public string StatisticName { get; private set; }
        public string Condition { get; set; }
        public string StatisticType { get; set; }

        public DelegateStatModifier(string statName, string type, Func<float> calculation)
            : this(statName, type)
        {
            this.Calculation = calculation;
        }

        protected DelegateStatModifier(string statName, string type)
        {
            this.ModifierType = type;
            this.StatisticName = statName;
        }
    }
}