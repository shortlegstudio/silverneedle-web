// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    [TestFixture]
    public class WeaponTrainingTests
    {
        [Fact]
        public void RegisterWeaponModifiersWithOffenseStats()
        {
            var abilities = new AbilityScores();
            var size = new SizeStats();
            var inventory = new Inventory();
            var offStat = new OffenseStats();
            var components = new ComponentBag();
            components.Add(abilities, size, inventory, offStat);

            var weaponTraining = new WeaponTraining(WeaponGroup.LightBlades, 1);
            weaponTraining.Initialize(components);
            var mods = offStat.WeaponModifiers;
            Assert.That(mods, Contains.Item(weaponTraining.WeaponAttackBonus));
            Assert.That(mods, Contains.Item(weaponTraining.WeaponDamageBonus));
            Assert.That(weaponTraining.WeaponAttackBonus.Modifier, Is.EqualTo(1));
            Assert.That(weaponTraining.WeaponDamageBonus.Modifier, Is.EqualTo(1));
            Assert.That(weaponTraining.Level, Is.EqualTo(1));
        }

        [Fact]
        public void CreatesANiceNameForTheAbility()
        {
            var training = new WeaponTraining(WeaponGroup.Axes, 3);
            Assert.That(training.Name, Is.EqualTo("Weapon Training (Axes +3)"));
        }
    }
}