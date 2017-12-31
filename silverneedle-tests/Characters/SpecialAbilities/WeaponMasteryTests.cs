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

    
    public class WeaponMasteryTests : RequiresDataFiles
    {
        [Fact]
        public void AddsWeaponCriticalDamageModifierToOffenseStats()
        {
            var character = CharacterTestTemplates.AverageBob();
            var offStats = character.Get<OffenseStats>();
            var master = new WeaponMastery();
            character.Add(master);
            Assert.Contains(master.WeaponCriticalDamageBonus, offStats.WeaponModifiers);
        }

        [Fact]
        public void WorksWithMasterworkVersionsOfWeapons()
        {
            var character = CharacterTestTemplates.AverageBob();
            var offStats = character.Get<OffenseStats>();
            var master = new WeaponMastery();
            character.Add(master);
            var weapon = master.Weapon;
            var masterworkWeapon = new MasterworkWeapon(weapon);
            Assert.Contains(master.WeaponCriticalDamageBonus, offStats.WeaponModifiers);
            Assert.True(master.WeaponCriticalDamageBonus.WeaponQualifies(masterworkWeapon));
        }
    }
}