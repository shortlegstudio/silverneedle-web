// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Equipment;
    public class WeaponAttackModifier : IValueStatModifier, IWeaponModifier
    {
        public float Modifier 
        { 
            get { return ModifierCalculation(); }
        }

        public string Reason { get; private set; }

        public string ModifierType { get { return "Bonus"; } }
        public string Condition { get; set; }

        public string StatisticName { get { return "Weapon Attack"; } }
        public string StatisticType { get; set; }

        public WeaponAttackModifier(string reason, float modifier, Func<IWeaponAttackStatistics, bool> weaponQualifies)
        {
            this.staticModifier = modifier;
            ModifierCalculation = () => { return this.staticModifier; };
            this.Reason = reason;
            this.WeaponQualifies = weaponQualifies;
        }

        public WeaponAttackModifier(string reason, Func<float> modifier, Func<IWeaponAttackStatistics, bool> weaponQualifies)
        {
            ModifierCalculation = modifier;
            this.Reason = reason;
            this.WeaponQualifies = weaponQualifies;
        }

        public Func<IWeaponAttackStatistics, bool> WeaponQualifies { get; private set; }
        private Func<float> ModifierCalculation { get; set; }

        public void ApplyModifier(WeaponAttack attack)
        {
            attack.AttackBonus.AddModifier(this);
        }

        private float staticModifier;
    }
}