// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Equipment;
    public class WeaponDamageModifier : IValueStatModifier, IWeaponModifier
    {
        private float staticModifier;
        public float Modifier { get { return ModifierCalculation(); } }

        public string ModifierType { get { return "Bonus"; } }

        public string StatisticName { get { return "Weapon Damage"; } }
        public string Condition { get; set; }
        public string StatisticType { get; set; }
        public WeaponDamageModifier(float modifier, Func<IWeaponAttackStatistics, bool> weaponQualifies)
        {
            this.staticModifier = modifier;
            this.ModifierCalculation = () => { return staticModifier; };
            this.WeaponQualifies = weaponQualifies;
        }

        public WeaponDamageModifier(Func<float> modifier, Func<IWeaponAttackStatistics, bool> weaponQualifies)
        {
            this.ModifierCalculation = modifier;
            this.WeaponQualifies = weaponQualifies;
        }

        public Func<IWeaponAttackStatistics, bool> WeaponQualifies { get; private set; }
        private Func<float> ModifierCalculation;

        public void ApplyModifier(WeaponAttack attack)
        {
            attack.DamageModifier.AddModifier(this);
        }
    }
}