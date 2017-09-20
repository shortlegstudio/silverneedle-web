// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Equipment;

    public class MeleeAttackTests
    {
        private Weapon sword;
        public MeleeAttackTests()
        {
            sword = new Weapon(
                "Sword",
                1,
                "1d6",
                DamageTypes.Slashing,
                19,
                3,
                0,
                WeaponType.OneHanded,
                WeaponGroup.LightBlades,
                WeaponTrainingLevel.Martial
            );
        }

        [Fact]
        public void UsesACharactersMeleeAttackBonus()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var melee = new MeleeAttack(bob, sword);
            Assert.Equal(bob.Offense.MeleeAttackBonus.TotalValue, melee.AttackBonus);

            var oldbonus = melee.AttackBonus;
            bob.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            Assert.Equal(oldbonus + 3, melee.AttackBonus);
        }

        [Fact]
        public void UsesCharactersStrengthToAddBonusDamage()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var melee = new MeleeAttack(bob, sword);
            Assert.Equal("1d6", melee.Damage.ToString());

            bob.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            Assert.Equal("1d6+3", melee.Damage.ToString());
        }

        [Fact]
        public void SomePropertiesAreJustAssignedByWeapon()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var melee = new MeleeAttack(bob, sword);
            Assert.Equal("Slashing", melee.DamageType);
            Assert.Equal(3, melee.CriticalModifier);
            Assert.Equal(19, melee.CriticalThreat);
            Assert.Equal("Sword", melee.Name);
        }
    }
}