// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    public class MeleeAttack : WeaponAttack
    {
        private AbilityScore strength;
        public MeleeAttack(OffenseStats offenseAbilities,
            AbilityScore strength, 
            CharacterSize size,
            IWeaponAttackStatistics weapon) : base(offenseAbilities, size, weapon)
        {
            this.strength = strength;
            AttackBonus.AddModifier(new StatisticStatModifier("Melee Attack Bonus", offenseAbilities.MeleeAttackBonus));
            DamageModifier.AddModifier(this.strength.UniversalStatModifier);
            this.AttackType = AttackTypes.Melee;
        }
    }
}