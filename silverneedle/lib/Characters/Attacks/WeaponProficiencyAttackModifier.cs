// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Equipment;
    public class WeaponProficiencyAttackModifier : DelegateStatModifier

    {
        public WeaponProficiencyAttackModifier(OffenseStats offenseAbilities, IWeaponAttackStatistics weapon)
            : base(weapon.Name + " Proficiency Modifier", 
                "proficiency penalty", 
                "Level of Proficiency") 
        {
            this.offenseAbilities = offenseAbilities;
            this.weapon = weapon;
            this.Calculation = () => { return this.offenseAbilities.IsProficient(weapon) ? 0 : -4; }; 
        }
        private OffenseStats offenseAbilities;

        private IWeaponAttackStatistics weapon;

    }
}