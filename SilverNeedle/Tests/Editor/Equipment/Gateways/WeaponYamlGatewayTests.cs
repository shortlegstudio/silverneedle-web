using System;
using System.Linq;
using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Equipment;
using SilverNeedle;
using SilverNeedle.Equipment.Gateways;
using SilverNeedle.Yaml;

namespace Equipment {

	[TestFixture]
	public class WeaponYamlGatewayTests {
		[Test]
		public void AllImportantStatsForALongSwordAreAvailable() {
			var repo = new WeaponYamlGateway (WeaponYamlFile.ParseYaml());
			var weapons = repo.All ();
			var longsword = weapons.First();
			Assert.AreEqual ("Longsword", longsword.Name);
			Assert.AreEqual ("1d8", longsword.Damage);
			Assert.AreEqual (4, longsword.Weight);
			Assert.AreEqual (19, longsword.CriticalThreat);
			Assert.AreEqual (3, longsword.CriticalModifier);
			Assert.AreEqual (DamageTypes.Slashing, longsword.DamageType);
			Assert.AreEqual (WeaponType.OneHanded, longsword.Type);
			Assert.AreEqual (WeaponGroup.HeavyBlades, longsword.Group);
			Assert.AreEqual (WeaponTrainingLevel.Martial, longsword.Level);
		}

		[Test]
		public void AllImportantStatsForADaggerAreAvailable() {
			var repo = new WeaponYamlGateway (WeaponYamlFile.ParseYaml());
			var dagger = repo.All().Last();
			Assert.AreEqual ("Dagger", dagger.Name);
			Assert.AreEqual ("1d4", dagger.Damage);
			Assert.AreEqual (1, dagger.Weight);
			Assert.AreEqual (20, dagger.CriticalThreat);
			Assert.AreEqual (2, dagger.CriticalModifier);
			Assert.AreEqual (DamageTypes.Piercing, dagger.DamageType);
			Assert.AreEqual (10, dagger.Range);
			Assert.AreEqual (WeaponType.Light, dagger.Type);
			Assert.AreEqual (WeaponGroup.LightBlades, dagger.Group);
			Assert.AreEqual (WeaponTrainingLevel.Simple, dagger.Level);
		}

		[Test]
		public void CanSelectWeaponsBasedOnProficiencies() {
			var repo = new WeaponYamlGateway(WeaponYamlFile.ParseYaml());
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
- weapon:
  name: Dagger
  damage: 1d4
  damage_type: Piercing
  weight: 1
  range: 10
  group: LightBlades
  type: Light
  training_level: Simple
";
	}
}
