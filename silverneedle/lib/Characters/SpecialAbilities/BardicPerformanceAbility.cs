// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.SpecialAbilities.BardicPerformances;
    using SilverNeedle.Utility;
    public class BardicPerformanceAbility : IAbility, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private const int BASE_ROUNDS_PER_DAY = 4;
        private BasicStat roundsPerDay = new BasicStat("Bardic Performance Rounds per Day", BASE_ROUNDS_PER_DAY);
        private ComponentContainer components;

        [AddToContainer]
        public IValueStatistic RoundsPerDayStatistic { get { return roundsPerDay; } }

        public int RoundsPerDay
        {
            get { return roundsPerDay.TotalValue; }
        }

        public IEnumerable<IBardicPerformance> Performances
        {
            get { return components.GetAll<IBardicPerformance>(); }
        }

        public void Initialize(ComponentContainer components)
        {
            this.components = components;
            var abilityScores = components.Get<AbilityScores>();
            roundsPerDay.AddModifier(abilityScores.GetStatModifier(AbilityScoreTypes.Charisma));
            var bardLevel = components.Get<ClassLevel>();
            roundsPerDay.AddModifier(
                new DelegateStatModifier(
                    roundsPerDay.Name,
                    "level-up",
                    () => { return (bardLevel.Level - 1) * 2; }
                )
            );

        }

        public string DisplayString()
        {
            return "Bardic Performance {0} rnds/day ({1})".Formatted(
                this.RoundsPerDay,
                string.Join(", ", this.Performances.Select(x => x.Description))
            );
        }
    }
}