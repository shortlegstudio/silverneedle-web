// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    public interface IAttackStatistic
    {
        string Name { get; }
        BasicStat AttackBonus { get; }
        BasicStat DamageModifier { get; }
        Cup Damage { get; }
        int NumberOfAttacks { get; }
        AttackTypes AttackType { get; }
        BasicStat CriticalModifier { get; }
        int CriticalThreat { get; }
        int SaveDC { get; }
    }
}