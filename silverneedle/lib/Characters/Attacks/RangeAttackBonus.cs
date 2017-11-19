// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Utility;
    public class RangeAttackBonus : IComponent, IStatistic
    {
        public string Name { get { return baseRangeAttackBonus.Name; } }

        public int TotalValue { get { return baseRangeAttackBonus.TotalValue; } }
        private BasicStat baseRangeAttackBonus = new BasicStat("Range Attack Bonus");

        public void AddModifier(IStatModifier modifier)
        {
            baseRangeAttackBonus.AddModifier(modifier);
        }

        public int GetConditionalValue(string condition)
        {
            return baseRangeAttackBonus.GetConditionalValue(condition);
        }

        public void Initialize(ComponentBag components)
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
    }
}