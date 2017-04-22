// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;

namespace SilverNeedle
{
    public class DelegateStatModifier : IStatModifier
    {
        public Func<float> Calculation;

        public float Modifier { get { return Calculation(); } }

        public string Reason { get; private set; }

        public string Type { get; private set; }

        public string StatisticName { get; private set; }

        public DelegateStatModifier(string statName, string type, string reason, Func<float> calculation)
        {
            this.Reason = reason;
            this.Type = type;
            this.StatisticName = statName;
            this.Calculation = calculation;
        }
    }
}