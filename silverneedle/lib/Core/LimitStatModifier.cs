// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class LimitStatModifier : IValueStatModifier, IComponent
    {
        private IValueStatistic maxValue;
        private IValueStatistic minValue; 

        public LimitStatModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }
        public float Modifier 
        {
            get { return Math.Min(minValue.TotalValue, maxValue.TotalValue); } 
        }

        [ObjectStore("modifier-type")]
        public string ModifierType { get; set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("condition")]
        public string Condition { get; set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; set; }

        [ObjectStore("min-value")]
        public string MinValueStatName { get; set; }
        [ObjectStore("max-value")]
        public string MaxValueStatName { get; set; }
        public ComponentContainer Parent { get; set; }

        public void Initialize(ComponentContainer components)
        {
            minValue = components.FindStat<IValueStatistic>(MinValueStatName);
            maxValue = components.FindStat<IValueStatistic>(MaxValueStatName);
        }
    }
}