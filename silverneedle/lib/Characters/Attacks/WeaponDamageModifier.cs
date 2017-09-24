// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Equipment;
    public class WeaponDamageModifier : IStatModifier, IWeaponModifier
    {
        public float Modifier { get; set; }

        public string Reason { get; private set; }

        public string Type { get { return "Bonus"; } }

        public string StatisticName { get { return "Weapon Damage"; } }
        public WeaponDamageModifier(string reason, float modifier, Func<IWeaponAttackStatistics, bool> weaponQualifies)
        {
            this.Modifier = modifier;
            this.Reason = reason;
            this.WeaponQualifies = weaponQualifies;
        }

        public Func<IWeaponAttackStatistics, bool> WeaponQualifies { get; private set; }

        public void ApplyModifier(WeaponAttack attack)
        {
            attack.DamageModifier.AddModifier(this);
        }
    }
}