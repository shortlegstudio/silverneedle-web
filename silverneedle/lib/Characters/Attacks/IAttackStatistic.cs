// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    public interface IAttackStatistic : IAttack
    {
        IStatistic AttackBonus { get; }
        int NumberOfAttacks { get; }
        IStatistic CriticalModifier { get; }
        int CriticalThreat { get; }
        int SaveDC { get; }
        int Range { get; }
        string AttackBonusString();
    }
}