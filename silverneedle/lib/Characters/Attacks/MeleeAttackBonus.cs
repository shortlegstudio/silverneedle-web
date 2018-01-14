// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Utility;
    public class MeleeAttackBonus : IComponent, IValueStatistic
    {
        public string Name { get { return baseMeleeAttackBonus.Name; } }

        public int TotalValue { get { return baseMeleeAttackBonus.TotalValue; } }
        private BasicStat baseMeleeAttackBonus = new BasicStat("Melee Attack Bonus");

        public int GetConditionalValue(string condition)
        {
            return baseMeleeAttackBonus.GetConditionalValue(condition);
        }

        public void AddModifier(IStatisticModifier modifier)
        {
            baseMeleeAttackBonus.AddModifier(modifier);
        }

        public void Initialize(ComponentContainer components)
        {
            var offenseStats = components.Get<OffenseStats>();
            var abilityScores = components.Get<AbilityScores>();
            var strength = abilityScores.GetAbility(AbilityScoreTypes.Strength);
            var sizeStats = components.Get<SizeStats>();
            baseMeleeAttackBonus.AddModifiers(
                new StatisticStatModifier(StatNames.MeleeAttackBonus, offenseStats.BaseAttackBonus),
                new AbilityStatModifier(strength),
                sizeStats.PositiveSizeModifier
            );
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}