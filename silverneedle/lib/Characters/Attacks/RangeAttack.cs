// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    public class RangeAttack : AttackStatistic
    {
        public RangeAttack(OffenseStats offenseAbilities,
            CharacterSize size,
            IWeapon weapon) : base(offenseAbilities, size, weapon)
        {
            AttackBonus.AddModifier(new StatisticStatModifier("Range Attack Bonus", offenseAbilities.RangeAttackBonus));
            this.AttackType = AttackTypes.Ranged;
        }
    }
}