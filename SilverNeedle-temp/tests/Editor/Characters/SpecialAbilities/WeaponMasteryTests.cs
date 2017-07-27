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
    public class WeaponMasteryTests
    {
        [Fact]
        public void AddsWeaponCriticalDamageModifierToOffenseStats()
        {
            var offStats = new OffenseStats();
            var components = new ComponentBag();
            components.Add(offStats);
            var weapon = new Weapon();

            var master = new WeaponMastery(weapon);
            master.Initialize(components);
            Assert.That(offStats.WeaponModifiers, Contains.Item(master.WeaponCriticalDamageBonus));
        }

        [Fact]
        public void WorksWithMasterworkVersionsOfWeapons()
        {
            var offStats = new OffenseStats();
            var components = new ComponentBag();
            components.Add(offStats);
            var weapon = new Weapon();
            var master = new WeaponMastery(weapon);
            var masterworkWeapon = new MasterworkWeapon(weapon);
            master.Initialize(components);
            Assert.That(offStats.WeaponModifiers, Contains.Item(master.WeaponCriticalDamageBonus));
            Assert.That(master.WeaponCriticalDamageBonus.WeaponQualifies(masterworkWeapon));
        }
    }
}