// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;

namespace SilverNeedle
{
    public class StatisticStatModifier : IStatModifier
    {
        private BasicStat statistic;


        public StatisticStatModifier(string statisticName, BasicStat trackingStat) : this(statisticName, trackingStat.Name, trackingStat.Name, trackingStat)
        {

        }
        public StatisticStatModifier(string statisticName, string type, string reason, BasicStat trackingStat)
        {
            statistic = trackingStat;
            this.StatisticName = statisticName;
            this.Type = type;
            this.Reason = reason;
        }

        public float Modifier { get { return this.statistic.TotalValue; } }

        public string Reason { get; private set; }

        public string Type { get; private set; }

        public string StatisticName { get; private set; }
    }
}
