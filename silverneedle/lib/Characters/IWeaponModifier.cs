// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Equipment;
    using SilverNeedle.Characters.Attacks;
    public interface IWeaponModifier
    {
        Func<IWeaponAttackStatistics, bool> WeaponQualifies { get; }
        void ApplyModifier(WeaponAttack attack);
    }
}