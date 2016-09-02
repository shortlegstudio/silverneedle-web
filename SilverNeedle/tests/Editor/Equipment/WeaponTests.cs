using NUnit.Framework;
using SilverNeedle.Equipment;


namespace Equipment {

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
				WeaponTrainingLevel.Simple
			);
			Assert.AreEqual (20, wpn.CriticalThreat);
			Assert.AreEqual (2, wpn.CriticalModifier);
		}

		[Test]
		public void LightWeaponsWithNoRangeAreMeleeOnly() {
			var wpn = new Weapon ();
			wpn.Type = WeaponType.Light;
			Assert.IsFalse (wpn.IsRanged);
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
			var wpn = new Weapon ();
			wpn.Type = WeaponType.Ranged;
			Assert.IsTrue (wpn.IsRanged);
			Assert.IsFalse (wpn.IsMelee);
		}
	}
}
