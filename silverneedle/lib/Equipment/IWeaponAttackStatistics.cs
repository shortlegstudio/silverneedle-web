// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public interface IWeaponAttackStatistics : INamedEntity
    {
        string Damage { get; }
        DamageTypes DamageType { get; }
        int CriticalThreat { get; }
        int CriticalModifier { get; }
        int Range { get; }
        WeaponType Type { get; }
        WeaponTrainingLevel Level { get; }
        int AttackModifier { get; }

        WeaponGroup Group { get; }
        string ProficiencyName { get; }
        bool IsRanged { get; }
        bool IsMelee { get; }

    }
}