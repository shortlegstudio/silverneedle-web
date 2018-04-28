// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class WeaponMastery : IAbility, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public IWeapon Weapon { get; private set; }
        public WeaponCriticalDamageModifier WeaponCriticalDamageBonus { get; private set; }

        public WeaponMastery()
        {
            this.WeaponCriticalDamageBonus = new WeaponCriticalDamageModifier(1, Qualifies);
        }

        private bool Qualifies(IWeaponAttackStatistics weapon)
        {
            return weapon.ProficiencyName.EqualsIgnoreCase(this.Weapon.ProficiencyName);
        }


        public void Initialize(ComponentContainer components)
        {
            this.Weapon = GatewayProvider.Get<Weapon>().ChooseOne();
            components.Get<OffenseStats>().AddWeaponModifier(this.WeaponCriticalDamageBonus);
        }

        public string DisplayString()
        {
            return string.Format("Weapon Mastery ({0})", Weapon.ProficiencyName);
        }
    }
}