// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class StatisticStatModifier : IStatModifier, IComponent
    {
        private IStatistic statistic;

        public StatisticStatModifier(string statisticName, IStatistic trackingStat) : this(statisticName, trackingStat.Name, trackingStat.Name, trackingStat)
        {

        }
        public StatisticStatModifier(string statisticName, string type, string reason, IStatistic trackingStat)
        {
            statistic = trackingStat;
            this.StatisticName = statisticName;
            this.Type = type;
            this.Reason = reason;
            this.TrackingStatName = trackingStat.Name;
        }

        public StatisticStatModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public float Modifier { get { return this.statistic.TotalValue; } }

        [ObjectStoreOptional("reason")]
        public string Reason { get; private set; }

        [ObjectStore("modifier-type")]
        public string Type { get; private set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStore("condition")]
        public string Condition { get; set; }

        [ObjectStore("modifier")]
        public string TrackingStatName { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            if(statistic == null)
            {
                statistic = components.FindStat(TrackingStatName);
            }
        }
    }
}
