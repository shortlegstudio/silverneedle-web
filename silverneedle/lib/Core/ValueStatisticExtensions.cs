// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.Linq;
    using SilverNeedle.Utility;

    public static class ValueStatisticExtensions
    {
        public static string DisplayModifierValues(this IValueStatistic statistic)
        {
            var result = statistic.TotalValue.ToModifierString();
            var conditional = statistic.GetConditions().Select(x => "{0} {1}".Formatted(statistic.GetConditionalValue(x).ToModifierString(), x));
            if(conditional.NotEmpty())
            {
                return "{0} ({1})".Formatted(result, string.Join(", ", conditional));
            }

            return result;
        }
    }
}