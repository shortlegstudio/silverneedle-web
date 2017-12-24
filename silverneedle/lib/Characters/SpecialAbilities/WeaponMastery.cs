// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    public class WeaponMastery : SpecialAbility, IComponent
    {
        public IWeapon Weapon { get; private set; }
        public WeaponCriticalDamageModifier WeaponCriticalDamageBonus { get; private set; }

        public WeaponMastery(IWeapon weapon)
        {
            this.Weapon = weapon;
            this.WeaponCriticalDamageBonus = new WeaponCriticalDamageModifier("Weapon Mastery", 1, wpn => 
                { return wpn.ProficiencyName.EqualsIgnoreCase(Weapon.ProficiencyName); });
            this.Name = string.Format("Weapon Mastery ({0})", Weapon.ProficiencyName);
        }


        public void Initialize(ComponentContainer components)
        {
            components.Get<OffenseStats>().AddWeaponModifier(this.WeaponCriticalDamageBonus);
        }
    }
}