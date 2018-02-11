// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System;
    using SilverNeedle.Characters;

    /// <summary>
    /// Ability stat modifier will represent the modifiers for a stat that affects
    /// a statistic. For example applying constitution bonus to a fortitude save
    /// </summary>
    public class AbilityStatModifier : StatisticStatModifier
    {
        public AbilityStatModifier(AbilityScore ability) : base("any", ability.ModifierStat)
        {
        }

        public AbilityStatModifier(AbilityScore ability, string statisticName) : base(statisticName, ability.ModifierStat)
        {
        }
    }
}