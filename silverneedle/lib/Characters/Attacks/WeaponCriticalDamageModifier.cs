// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Equipment;
    public class WeaponCriticalDamageModifier : IStatModifier, IWeaponModifier
    {

        public WeaponCriticalDamageModifier(string reason, float modifier, Func<IWeaponAttackStatistics, bool> qualify)
        {
            this.Reason = reason;
            this.Modifier = modifier;
            this.WeaponQualifies = qualify;
        }
        public Func<IWeaponAttackStatistics, bool> WeaponQualifies { get; private set; }

        public float Modifier { get; set; }

        public string Reason { get; private set; }

        public string Type { get { return "Bonus"; } }

        public string StatisticName { get { return "Weapon Critical"; } }

        public void ApplyModifier(WeaponAttack attack)
        {
            attack.CriticalModifier.AddModifier(this);
        }
    }
}