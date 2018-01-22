// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class StatisticStatModifier : IValueStatModifier, IComponent
    {
        private IValueStatistic statistic;

        public StatisticStatModifier(string statisticName, IValueStatistic trackingStat) : this(statisticName, trackingStat.Name, trackingStat)
        {

        }
        public StatisticStatModifier(string statisticName, string type, IValueStatistic trackingStat)
        {
            statistic = trackingStat;
            this.StatisticName = statisticName;
            this.ModifierType = type;
            this.TrackingStatName = trackingStat.Name;
        }

        public StatisticStatModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public float Modifier { get { return this.statistic.TotalValue; } }


        [ObjectStore("modifier-type")]
        public string ModifierType { get; private set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("condition")]
        public string Condition { get; set; }

        [ObjectStore("modifier")]
        public string TrackingStatName { get; private set; }

        [ObjectStoreOptional("stat-type")]
        public string StatisticType { get; set; }

        public void Initialize(ComponentContainer components)
        {
            if(statistic == null)
            {
                statistic = components.FindStat<IValueStatistic>(TrackingStatName);
            }
        }
    }
}
