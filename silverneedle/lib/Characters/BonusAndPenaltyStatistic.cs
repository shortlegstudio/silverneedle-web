// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;

    public class BonusAndPenaltyStatistic : IAbility
    {

        [ObjectStore("name")]
        public string Name { get; private set; }
        [ObjectStore("bonus")]
        public string BonusStatistic { get; private set; }

        [AddToContainer]
        public IValueStatistic Bonus { get; private set; }
        [AddToContainer]
        public IValueStatistic Penalty { get; private set; }
        [ObjectStore("penalty")]
        public string PenaltyStatistic { get; private set; }

        public BonusAndPenaltyStatistic(IObjectStore configuration)
        {
            configuration.Deserialize(this);
            Bonus = new BasicStat("{0} Bonus".Formatted(this.Name), configuration.GetInteger("bonus-base-value"));
            Penalty = new BasicStat("{0} Penalty".Formatted(this.Name), configuration.GetInteger("penalty-base-value"));
        }

        public string DisplayString()
        {
            return "{0} ({1} {2} /{3} {4})".Formatted(
                Name,
                Penalty.ToString(),
                PenaltyStatistic,
                Bonus.ToString(),
                BonusStatistic
            );
        }
    }
}