// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class LoreMaster : IAbility, IComponent, INameByType, IUsesPerDay
    {
        private IStatistic usesPerDay;
        public int UsesPerDay
        {
            get 
            {
                return usesPerDay.TotalValue;
            }
        }
        public void Initialize(ComponentContainer components)
        {
            usesPerDay = components.FindStat(this.UsesPerDayStatName());
        }

        public string DisplayString()
        {
            return "{0} {1}/day".Formatted(this.Name(), UsesPerDay);
        }
    }
}