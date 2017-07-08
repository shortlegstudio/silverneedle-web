// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    public class WeaponTraining : SpecialAbility, IComponent
    {
        public WeaponTraining(WeaponGroup group, int level)
        {
            this.Group = group;
            this.Level = level;
            QualifyCheck = x => { return x.Group == this.Group; };
            UpdateName();
            WeaponAttackBonus = new WeaponAttackModifier(
                "Weapon Training",
                level,
                QualifyCheck
            );
            WeaponDamageBonus = new WeaponDamageModifier(
                "Weapon Training",
                level,
                QualifyCheck
            );
        }

        Func<IWeapon, bool> QualifyCheck;
        public WeaponGroup Group { get; private set; }
        public int Level { get; private set; }

        public WeaponAttackModifier WeaponAttackBonus { get; private set; }
        public WeaponDamageModifier WeaponDamageBonus { get; private set; }
        public void Initialize(ComponentBag components)
        {
            var offStats = components.Get<OffenseStats>();
            offStats.AddWeaponModifier(WeaponAttackBonus);
            offStats.AddWeaponModifier(WeaponDamageBonus);
        }

        public void SetLevel(int level)
        {
            Level = level;
            this.WeaponAttackBonus.Modifier = level;
            this.WeaponDamageBonus.Modifier = level;
            UpdateName();
        }

        private void UpdateName()
        {
            this.Name = string.Format("Weapon Training ({0} +{1})", this.Group, this.Level);
        }
    }
}