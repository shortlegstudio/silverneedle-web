// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Characters.Attacks;
    public class WeaponAttackTests
    {
        [Fact]
        public void IsAwareOfMultipleAttacksIfBABAllows()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.BaseAttackBonus.SetValue(6);
            bob.Offense.AddWeaponProficiency("Sword");
            var sword = new Weapon();
            sword.Name = "Sword";
            sword.Damage = "1d6";
            var attack = new MeleeAttack(bob.Offense, bob.AbilityScores.GetAbility(AbilityScoreTypes.Strength), bob.Size.Size, sword);
            Assert.Equal(2, attack.NumberOfAttacks);
            Assert.Contains("Sword +6/+1", attack.ToString());
        }
    }
}