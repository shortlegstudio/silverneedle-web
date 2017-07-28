// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    
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
            Assert.Contains(master.WeaponCriticalDamageBonus, offStats.WeaponModifiers);
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
            Assert.Contains(master.WeaponCriticalDamageBonus, offStats.WeaponModifiers);
            Assert.True(master.WeaponCriticalDamageBonus.WeaponQualifies(masterworkWeapon));
        }
    }
}