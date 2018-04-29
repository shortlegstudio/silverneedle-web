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

    public class MeleeAttackTests : RequiresDataFiles
    {
        private Weapon sword;
        private IWeapon magicHammer;
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

            magicHammer = new Weapon(
                "Hammer",
                1,
                "1d8",
                DamageTypes.Slashing,
                20,
                2,
                0,
                WeaponType.OneHanded,
                WeaponGroup.Hammers,
                WeaponTrainingLevel.Simple
            );
            magicHammer = new MagicWeapon(magicHammer, 1);
        }

        [Fact]
        public void UsesACharactersMeleeAttackBonus()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.AddWeaponProficiency(sword.ProficiencyName);
            var melee = new MeleeAttack(bob.Offense, 
                bob.AbilityScores.GetAbility(AbilityScoreTypes.Strength),
                CharacterSize.Medium,
                sword);
            Assert.Equal(bob.Offense.MeleeAttackBonus.TotalValue, melee.AttackBonus.TotalValue);

            var oldbonus = melee.AttackBonus.TotalValue;
            bob.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            Assert.Equal(oldbonus + 3, melee.AttackBonus.TotalValue);
        }

        [Fact]
        public void UsesCharactersStrengthToAddBonusDamage()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.AddWeaponProficiency(sword.ProficiencyName);
            var melee = new MeleeAttack(bob.Offense, 
                bob.AbilityScores.GetAbility(AbilityScoreTypes.Strength),
                CharacterSize.Medium, 
                sword);
            Assert.Equal("1d6", melee.Damage.ToString());

            bob.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            Assert.Equal("1d6+3", melee.Damage.ToString());
        }

        [Fact]
        public void SomePropertiesAreJustAssignedByWeapon()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.AddWeaponProficiency(sword.ProficiencyName);
            var melee = new MeleeAttack(bob.Offense, 
                bob.AbilityScores.GetAbility(AbilityScoreTypes.Strength),
                CharacterSize.Medium,
                sword);
            Assert.Equal("Slashing", melee.DamageType);
            Assert.Equal(3, melee.CriticalModifier.TotalValue);
            Assert.Equal(19, melee.CriticalThreat);
            Assert.Equal("Sword", melee.Name);
        }

        [Fact]
        public void ConvertsDamageBasedOnCharacterSize()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Offense.AddWeaponProficiency(sword.ProficiencyName);
            var melee = new MeleeAttack(bob.Offense,
                bob.AbilityScores.GetAbility(AbilityScoreTypes.Strength),
                CharacterSize.Small,
                sword);
            Assert.Equal("1d4", melee.Damage.ToString());
        }

        [Fact]
        public void MagicWeaponsAdjustDamageAndAttackBonusInPredictableWays()
        {
            var bad = CharacterTestTemplates.StrongBad();
            bad.Offense.AddWeaponProficiency(magicHammer.ProficiencyName);
            var melee = new MeleeAttack(
                bad.Offense,
                bad.AbilityScores.GetAbility(AbilityScoreTypes.Strength),
                CharacterSize.Medium,
                magicHammer
            );
            Assert.Equal("1d8+4", melee.Damage.ToString());
            Assert.Equal(4, melee.AttackBonus.TotalValue);
        }

        [Fact]
        public void CustomAttackBonusModifiersAdjustAttackBonusForWeapon()
        {
            var bad = CharacterTestTemplates.StrongBad();
            bad.Offense.AddWeaponProficiency(sword.ProficiencyName);
            bad.Add(new WeaponAttackModifier(1, (IWeaponAttackStatistics wpn) => { return wpn == sword; }));
            var melee = new MeleeAttack(
                bad.Offense,
                bad.AbilityScores.GetAbility(AbilityScoreTypes.Strength),
                CharacterSize.Medium,
                sword
            );
            Assert.Equal(4, melee.AttackBonus.TotalValue);
        }

        [Fact]
        public void IfNotProficientIssueAMinusFourPenalty()
        {
            var bad = CharacterTestTemplates.StrongBad();
            var melee = new MeleeAttack(
                bad.Offense,
                bad.AbilityScores.GetAbility(AbilityScoreTypes.Strength),
                CharacterSize.Medium,
                sword
            );
            Assert.Equal(-1, melee.AttackBonus.TotalValue);
        }
    }
}