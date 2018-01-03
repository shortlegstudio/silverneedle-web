// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;

    public class AbilityPrerequisite : IPrerequisite
    {
        public AbilityPrerequisite(AbilityScoreTypes ability, int value)
        {
            this.Ability = ability;
            this.Minimum = value;
        }

        public AbilityScoreTypes Ability { get; set; }

        public int Minimum { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            var ability = components.FindStat(this.Ability.ToString());
            return ability.TotalValue >= this.Minimum;
        }
    }
}