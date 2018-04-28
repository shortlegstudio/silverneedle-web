// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System.Collections.Generic;
    using SilverNeedle.Utility;

    public class RangeAttackBonus : IComponent, IValueStatistic
    {
        public ComponentContainer Parent { get; set; }
        public string Name { get { return baseRangeAttackBonus.Name; } }

        public int TotalValue { get { return baseRangeAttackBonus.TotalValue; } }
        private BasicStat baseRangeAttackBonus = new BasicStat("Range Attack Bonus");

        public void AddModifier(IStatisticModifier modifier)
        {
            baseRangeAttackBonus.AddModifier(modifier);
        }

        public IEnumerable<IStatisticModifier> Modifiers { get { return baseRangeAttackBonus.Modifiers; } }
        public void RemoveModifier(IStatisticModifier modifier)
        {
            baseRangeAttackBonus.RemoveModifier(modifier);
        }

        public IEnumerable<string> GetConditions()
        {
            return baseRangeAttackBonus.GetConditions();
        }

        public int GetConditionalValue(string condition)
        {
            return baseRangeAttackBonus.GetConditionalValue(condition);
        }

        public void Initialize(ComponentContainer components)
        {
            var offenseStats = components.Get<OffenseStats>();
            var abilityScores = components.Get<AbilityScores>();
            var dexterity = abilityScores.GetAbility(AbilityScoreTypes.Dexterity);
            var sizeStats = components.Get<SizeStats>();
            baseRangeAttackBonus.AddModifiers(
                new StatisticStatModifier(StatNames.RangeAttackBonus, offenseStats.BaseAttackBonus),
                dexterity.UniversalStatModifier,
                sizeStats.PositiveSizeModifier
            );
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}