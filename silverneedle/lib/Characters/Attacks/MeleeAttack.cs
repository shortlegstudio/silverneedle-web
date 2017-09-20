// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    public class MeleeAttack : AttackStatistic
    {
        private BasicStat meleeAttackBonus;
        private AbilityScore strength;
        public MeleeAttack(CharacterSheet character, IWeapon weapon) : base(weapon)
        {
            this.strength = character.AbilityScores.GetAbility(AbilityScoreTypes.Strength);
            this.meleeAttackBonus = character.Offense.MeleeAttackBonus;
            this.Damage = DiceStrings.ParseDice(weapon.Damage);
        }

        public override Cup Damage
        {
            get
            {
                base.Damage.Modifier = strength.TotalModifier;
                return base.Damage;
            }
        }

        public override int AttackBonus { get { return meleeAttackBonus.TotalValue; } }
    }
}