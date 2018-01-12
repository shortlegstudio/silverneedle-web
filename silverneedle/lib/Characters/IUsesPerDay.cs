// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    public interface IUsesPerDay
    {
        int UsesPerDay { get; }
    }


    public static class IUsesPerDayExtensions
    {
        public static string UsesPerDayStatName(this IUsesPerDay ability)
        {
            return "{0} Uses Per Day".Formatted(ability.GetType().Name.Titlize());
        }
    }
}