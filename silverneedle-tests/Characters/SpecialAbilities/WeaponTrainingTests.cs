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
            Assert.Contains(weaponTraining.WeaponAttackBonus, mods);
            Assert.Contains(weaponTraining.WeaponDamageBonus, mods);
            Assert.Equal(weaponTraining.WeaponAttackBonus.Modifier, 1);
            Assert.Equal(weaponTraining.WeaponDamageBonus.Modifier, 1);
            Assert.Equal(weaponTraining.Level, 1);
        }

        [Fact]
        public void CreatesANiceNameForTheAbility()
        {
            var training = new WeaponTraining(WeaponGroup.Axes, 3);
            Assert.Equal(training.Name, "Weapon Training (Axes +3)");
        }
    }
}