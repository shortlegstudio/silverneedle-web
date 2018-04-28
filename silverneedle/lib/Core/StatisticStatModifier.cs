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
        public ComponentContainer Parent { get; set; }
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
            this.Multiplier = 1;
        }

        public StatisticStatModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public virtual float Modifier 
        { 
            get 
            { 
                if(Every != 0 && Add != 0)
                {
                    var count = MathHelpers.FloorToInt(this.statistic.TotalValue / Every);
                    return count * Add;
                }
                
                return this.statistic.TotalValue * this.Multiplier; 
            } 
        }


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

        [ObjectStoreOptional("multiplier", 1.0f)]
        public float Multiplier { get; private set; }

        [ObjectStoreOptional("every", 0)]
        public int Every { get; private set; }

        [ObjectStoreOptional("add", 0)]
        public int Add { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            if(statistic == null)
            {
                statistic = components.FindStat<IValueStatistic>(TrackingStatName);
                if(statistic == null)
                    throw new StatisticNotFoundException(TrackingStatName);
            }
        }
    }
}
