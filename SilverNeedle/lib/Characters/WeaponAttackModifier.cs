// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Equipment;
    public class WeaponAttackModifier : IStatModifier, IWeaponModifier
    {
        public float Modifier { get; private set; }

        public string Reason { get; private set; }

        public string Type { get { return "Bonus"; } }

        public string StatisticName { get { return "Weapon Attack"; } }

        public WeaponAttackModifier(string reason, float modifier, Func<Weapon, bool> weaponQualifies)
        {
            this.Modifier = modifier;
            this.Reason = reason;
            this.WeaponQualifies = weaponQualifies;
        }

        public Func<Weapon, bool> WeaponQualifies { get; private set; }

        public void ApplyModifier(OffenseStats.AttackStatistic attack)
        {
            attack.AttackBonus += (int)Modifier;
        }
    }
}