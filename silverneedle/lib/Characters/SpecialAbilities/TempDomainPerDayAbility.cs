// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class TempDomainPerDayAbility : IAbility, IComponent, IUsesPerDay, INameByType
    {
        public ComponentContainer Parent { get; set; }
        private IValueStatistic wisdomModifier;

        public void Initialize(ComponentContainer components)
        {
            wisdomModifier = components.FindStat<IValueStatistic>("wisdom-modifier");
        }

        public int UsesPerDay
        {
            get { return 3 + wisdomModifier.TotalValue; }
        }

        public string DisplayString()
        {
            return "{0}/day - {1}".Formatted(this.UsesPerDay, this.Name());
        }
    }
}