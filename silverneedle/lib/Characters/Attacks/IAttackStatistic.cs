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
        IStatistic AttackBonus { get; }
        Cup Damage { get; }
        int NumberOfAttacks { get; }
        AttackTypes AttackType { get; }
        IStatistic CriticalModifier { get; }
        int CriticalThreat { get; }
        int SaveDC { get; }
        int Range { get; }
        string AttackBonusString();
        string DisplayString();
    }
}