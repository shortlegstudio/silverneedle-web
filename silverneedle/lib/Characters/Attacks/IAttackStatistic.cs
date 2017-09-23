// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    public interface IAttackStatistic
    {
        BasicStat AttackBonus { get; }
        BasicStat DamageModifier { get; }
        Cup Damage { get; }
    }
}