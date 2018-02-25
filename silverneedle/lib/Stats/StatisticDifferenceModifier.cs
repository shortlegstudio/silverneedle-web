// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class StatisticDifferenceModifier : IValueStatModifier, IComponent
    {
        private IValueStatistic _baseStatistic;
        private IValueStatistic _differenceStatistic;
        public float Modifier { get { return _differenceStatistic.TotalValue - _baseStatistic.TotalValue; } }

        [ObjectStore("modifier-type")]
        public string ModifierType { get; private set; }

        [ObjectStoreOptional("condition")]
        public string Condition { get; private set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; private set; }

        [ObjectStore("base-statistic")]
        public string BaseStatisticName { get; private set; }
        [ObjectStore("difference-statistic")]
        public string DifferenceStatisticName { get; private set; }

        public StatisticDifferenceModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public void Initialize(ComponentContainer components)
        {
            _baseStatistic = components.FindStat<IValueStatistic>(BaseStatisticName);
            _differenceStatistic = components.FindStat<IValueStatistic>(DifferenceStatisticName);
        }
    }
}