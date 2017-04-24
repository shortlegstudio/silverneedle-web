// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    public class WeaponMastery : SpecialAbility, IComponent
    {
        public Weapon Weapon { get; private set; }
        public WeaponCriticalDamageModifier WeaponCriticalDamageBonus { get; private set; }

        public WeaponMastery(Weapon weapon)
        {
            this.Weapon = weapon;
            this.WeaponCriticalDamageBonus = new WeaponCriticalDamageModifier("Weapon Mastery", 1, wpn => { return wpn.Name.EqualsIgnoreCase(Weapon.Name); });
            this.Name = string.Format("Weapon Mastery ({0})", Weapon.Name);
        }


        public void Initialize(ComponentBag components)
        {
            components.Get<OffenseStats>().AddWeaponModifier(this.WeaponCriticalDamageBonus);
        }
    }
}