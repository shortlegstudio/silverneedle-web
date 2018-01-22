// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Equipment;
    public class WeaponCriticalDamageModifier : IValueStatModifier, IWeaponModifier
    {

        public WeaponCriticalDamageModifier(float modifier, Func<IWeaponAttackStatistics, bool> qualify)
        {
            this.Modifier = modifier;
            this.WeaponQualifies = qualify;
        }
        public Func<IWeaponAttackStatistics, bool> WeaponQualifies { get; private set; }

        public float Modifier { get; set; }


        public string ModifierType { get { return "Bonus"; } }

        public string StatisticName { get { return "Weapon Critical"; } }
        public string StatisticType { get; set; }
        public string Condition { get; set; }

        public void ApplyModifier(WeaponAttack attack)
        {
            attack.CriticalModifier.AddModifier(this);
        }
    }
}