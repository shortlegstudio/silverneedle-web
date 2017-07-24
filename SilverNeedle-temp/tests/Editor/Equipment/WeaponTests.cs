// Copyright (c) 2017 Trevor Redfern
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment {
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class WeaponTests {		
        [Test]
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
            Assert.AreEqual(20, wpn.CriticalThreat);
            Assert.AreEqual(2, wpn.CriticalModifier);
        }

        [Test]
        public void LightWeaponsWithNoRangeAreMeleeOnly() {
            var wpn = new Weapon();
            wpn.Type = WeaponType.Light;
            Assert.IsFalse(wpn.IsRanged);
            Assert.IsTrue(wpn.IsMelee);
        }

        [Test]
        public void LightWeaponsWithSomeRangeAreBothRangedAndMelee() {
            var wpn = new Weapon ();
            wpn.Type = WeaponType.Light;
            wpn.Range = 10;
            Assert.IsTrue (wpn.IsRanged);
            Assert.IsTrue (wpn.IsMelee);
        }

        [Test]
        public void RangedWeaponsArentMeleeCompatible() {
            var wpn = new Weapon();
            wpn.Type = WeaponType.Ranged;
            Assert.IsTrue(wpn.IsRanged);
            Assert.IsFalse(wpn.IsMelee);
        }

        [Test]
        public void AllImportantStatsForALongSwordAreAvailable() {
            var repo = new EntityGateway<Weapon>(WeaponYamlFile.ParseYaml().Load<Weapon>());
            var weapons = repo.All ();
            var longsword = weapons.First();
            Assert.AreEqual("Longsword", longsword.Name);
            Assert.AreEqual("1d8", longsword.Damage);
            Assert.AreEqual(4, longsword.Weight);
            Assert.AreEqual(19, longsword.CriticalThreat);
            Assert.AreEqual(3, longsword.CriticalModifier);
            Assert.AreEqual(DamageTypes.Slashing, longsword.DamageType);
            Assert.AreEqual(WeaponType.OneHanded, longsword.Type);
            Assert.AreEqual(WeaponGroup.HeavyBlades, longsword.Group);
            Assert.AreEqual(WeaponTrainingLevel.Martial, longsword.Level);
            Assert.AreEqual(3200, longsword.Value);
        }

        [Test]
        public void AllImportantStatsForADaggerAreAvailable() {
            var repo = new EntityGateway<Weapon>(WeaponYamlFile.ParseYaml().Load<Weapon>());
            var dagger = repo.All().Last();
            Assert.AreEqual("Dagger", dagger.Name);
            Assert.AreEqual("1d4", dagger.Damage);
            Assert.AreEqual(1, dagger.Weight);
            Assert.AreEqual(20, dagger.CriticalThreat);
            Assert.AreEqual(2, dagger.CriticalModifier);
            Assert.AreEqual(DamageTypes.Piercing, dagger.DamageType);
            Assert.AreEqual(10, dagger.Range);
            Assert.AreEqual(WeaponType.Light, dagger.Type);
            Assert.AreEqual(WeaponGroup.LightBlades, dagger.Group);
            Assert.AreEqual(WeaponTrainingLevel.Simple, dagger.Level);
            Assert.AreEqual(40, dagger.Value);
        }

        [Test]
        public void CanSelectWeaponsBasedOnProficiencies() {
            var repo = new EntityGateway<Weapon>(WeaponYamlFile.ParseYaml().Load<Weapon>());
            var prof = new WeaponProficiency("dagger");

            var results = repo.FindByProficient(new WeaponProficiency[] { prof });
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("Dagger", results.First().Name);
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
