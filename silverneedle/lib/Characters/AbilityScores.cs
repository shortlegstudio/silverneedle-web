// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle;
    using SilverNeedle.Utility;

    public class AbilityScores : IComponent
    {
        public IEnumerable<AbilityScore> Abilities 
        {
            get { return this.Parent.GetAll<AbilityScore>(); }
        }

        public ComponentContainer Parent { get; set; }

        public AbilityScore GetAbility(AbilityScoreTypes ability)
        {
            return this.Parent.FindStat<AbilityScore>(ability.ToString());
        }

        public int GetScore(AbilityScoreTypes ability)
        {
            return this.GetAbility(ability).TotalValue;
        }

        public int GetScore(string ability)
        {
            return this.GetScore(AbilityScore.GetType(ability));
        }

        public void SetScore(AbilityScoreTypes ability, int val)
        {
            this.GetAbility(ability).SetValue(val);
        }

        public int GetModifier(AbilityScoreTypes ability)
        {
            return this.GetAbility(ability).TotalModifier;
        }

        public AbilityStatModifier GetStatModifier(AbilityScoreTypes ability)
        {
            return GetAbility(ability).UniversalStatModifier;
        }

        public void Initialize(ComponentContainer components)
        {
        }
    }
}