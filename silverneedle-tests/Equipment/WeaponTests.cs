// Copyright (c) 2017 Trevor Redfern
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment {
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public class WeaponTests {		
        [Fact]
        public void DefaultCriticalValuesForWeaponsAreTwentyAndTimesTwo() {
            var wpn = new Weapon (
            "Test",
            0,
            "1d8",
            DamageTypes.Piercing,
            0,
            0,
            0,
            WeaponType.Light,
            WeaponGroup.Axes,
            WeaponTrainingLevel.Simple);
            Assert.Equal(20, wpn.CriticalThreat);
            Assert.Equal(2, wpn.CriticalModifier);
        }

        [Fact]
        public void LightWeaponsWithNoRangeAreMeleeOnly() {
            var wpn = new Weapon();
            wpn.Type = WeaponType.Light;
            Assert.False(wpn.IsRanged);
            Assert.True(wpn.IsMelee);
        }

        [Fact]
        public void LightWeaponsWithSomeRangeAreBothRangedAndMelee() {
            var wpn = new Weapon ();
            wpn.Type = WeaponType.Light;
            wpn.Range = 10;
            Assert.True (wpn.IsRanged);
            Assert.True (wpn.IsMelee);
        }

        [Fact]
        public void RangedWeaponsArentMeleeCompatible() {
            var wpn = new Weapon();
            wpn.Type = WeaponType.Ranged;
            Assert.True(wpn.IsRanged);
            Assert.False(wpn.IsMelee);
        }

        [Fact]
        public void AllImportantStatsForALongSwordAreAvailable() {
            var repo = new EntityGateway<Weapon>(WeaponYamlFile.ParseYaml().Load<Weapon>());
            var weapons = repo.All ();
            var longsword = weapons.First();
            Assert.Equal("Longsword", longsword.Name);
            Assert.Equal("1d8", longsword.Damage);
            Assert.Equal(4, longsword.Weight);
            Assert.Equal(19, longsword.CriticalThreat);
            Assert.Equal(3, longsword.CriticalModifier);
            Assert.Equal(DamageTypes.Slashing, longsword.DamageType);
            Assert.Equal(WeaponType.OneHanded, longsword.Type);
            Assert.Equal(WeaponGroup.HeavyBlades, longsword.Group);
            Assert.Equal(WeaponTrainingLevel.Martial, longsword.Level);
            Assert.Equal(3200, longsword.Value);
        }

        [Fact]
        public void AllImportantStatsForADaggerAreAvailable() {
            var repo = new EntityGateway<Weapon>(WeaponYamlFile.ParseYaml().Load<Weapon>());
            var dagger = repo.All().Last();
            Assert.Equal("Dagger", dagger.Name);
            Assert.Equal("1d4", dagger.Damage);
            Assert.Equal(1, dagger.Weight);
            Assert.Equal(20, dagger.CriticalThreat);
            Assert.Equal(2, dagger.CriticalModifier);
            Assert.Equal(DamageTypes.Piercing, dagger.DamageType);
            Assert.Equal(10, dagger.Range);
            Assert.Equal(WeaponType.Light, dagger.Type);
            Assert.Equal(WeaponGroup.LightBlades, dagger.Group);
            Assert.Equal(WeaponTrainingLevel.Simple, dagger.Level);
            Assert.Equal(40, dagger.Value);
        }

        [Fact]
        public void CanSelectWeaponsBasedOnProficiencies() {
            var repo = new EntityGateway<Weapon>(WeaponYamlFile.ParseYaml().Load<Weapon>());
            var prof = new WeaponProficiency("dagger");

            var results = repo.FindByProficient(new WeaponProficiency[] { prof });
            Assert.Equal(1, results.Count());
            Assert.Equal("Dagger", results.First().Name);
        }

        private const string WeaponYamlFile = @"--- 
- weapon: 
  name: Longsword
  damage: 1d8
  damage_type: Slashing
  critical_threat: 19
  critical_modifier: 3
  weight: 4
  group: HeavyBlades
  type: OneHanded
  training_level: Martial
  cost: 32gp
- weapon:
  name: Dagger
  damage: 1d4
  damage_type: Piercing
  weight: 1
  range: 10
  group: LightBlades
  type: Light
  training_level: Simple
  cost: 4sp
";
	}
}
