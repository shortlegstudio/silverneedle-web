// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
  using System;
  using NUnit.Framework;
  using SilverNeedle;
  using SilverNeedle.Characters;
  using SilverNeedle.Equipment;

  [TestFixture]
	public class WeaponProficiencyTests {
		[Test]
		public void SimpleWeaponsAreProficientForSimple() {
			var prof = new WeaponProficiency("simple");
			var wpn = new Weapon();
			wpn.Level = WeaponTrainingLevel.Simple;
			Assert.IsTrue(prof.IsProficient(wpn));
			wpn.Level = WeaponTrainingLevel.Martial;
			Assert.IsFalse(prof.IsProficient(wpn));
		}

		[Test]
		public void MartialWeaponsAreProficientIfMartiallyTrained() {
			var prof = new WeaponProficiency("martial");
			var wpn = new Weapon();
			wpn.Level = WeaponTrainingLevel.Martial;
			Assert.IsTrue(prof.IsProficient(wpn));
		}

		[Test]
		public void MatchesBasedOnNameIfNotTrainingLevel() {
			var prof = new WeaponProficiency("Shortbow");
			var wpn = new Weapon();
			wpn.Name = "Longsword";
			Assert.IsFalse(prof.IsProficient(wpn));
			wpn.Name = "Shortbow";
			Assert.IsTrue(prof.IsProficient(wpn));
		}

        [Test]
        public void NameLooksHumanReadable() {
            var prof = new WeaponProficiency("simple");
            Assert.AreEqual("Simple weapons", prof.Name);
        }

		[Test]
		public void WeaponProficiencySupportsDynamicProperties()
		{
			var prof = new WeaponProficiency("(Tests.Characters.DynamicWeaponProf)");
			Assert.That(prof.Name, Is.EqualTo("Mace"));
		}
	}

	public class DynamicWeaponProf : IDynamicPropertyEvaluator
	{
		public string GetString()
		{
			return "mace";
		}
	}
}