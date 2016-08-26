using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters;
using SilverNeedle.Equipment;
using SilverNeedle.Dice;
using System.Collections;

namespace Characters {

	[TestFixture]
	public class OffenseStatsTests {
		OffenseStats smallStats;
		Inventory inventory;

		[SetUp]
		public void SetUp() {
			var abilities = new AbilityScores ();
			abilities.SetScore (AbilityScoreTypes.Strength, 16);
			abilities.SetScore (AbilityScoreTypes.Dexterity, 16);
			var size = new SizeStats (CharacterSize.Small, 1,1);

			inventory = new Inventory();
			smallStats = new OffenseStats (abilities, size, inventory);
		}

		[Test]
		public void BaseAttackBonusIsAStat() {
			Assert.IsInstanceOf<BasicStat> (smallStats.BaseAttackBonus);
		}

		[Test]
		public void BaseMeleeBonusIsBABAndStrengthAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (7, smallStats.MeleeAttackBonus());
		}

		[Test]
		public void BaseRangeBonusIsBABAndDexterityAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (7, smallStats.RangeAttackBonus());
		}

		[Test]
		public void CMBIsBABAndStrengthAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (5, smallStats.CombatManueverBonus ());
		}

		[Test]
		public void CMDIsBABStrengthAndDexterityAndSize() {
			smallStats.BaseAttackBonus.SetValue (3);
			Assert.AreEqual (18, smallStats.CombatManueverDefense ());
		}

		[Test]
		public void ModifiersCanBeAppliedToCombatManeuverDefense() {
			var mods = new MockMod();
			var oldCMD = smallStats.CombatManueverDefense();
			smallStats.ProcessModifier(mods);
			Assert.AreEqual(oldCMD + 1, smallStats.CombatManueverDefense());
		}

		[Test]
		public void ModifiersCanBeAppliedToCombatManeuverBonus() {
			var mods = new MockMod();
			var oldCMB = smallStats.CombatManueverBonus();
			smallStats.ProcessModifier(mods);
			Assert.AreEqual(oldCMB + 1, smallStats.CombatManueverBonus());
		}

		[Test]
		public void ContainsAListOfAllWeaponsAvailableAndTheirStats() {
			var longsword = Longsword();
			inventory.AddGear(longsword);
			Assert.AreEqual(1, smallStats.Attacks().Count);
			Assert.AreEqual("Longsword", smallStats.Attacks().First().Name);
			Assert.AreEqual(longsword, smallStats.Attacks().First().Weapon);
		}

		[Test]
		public void MeleeWeaponAttacksCalculateDamageBonuses() {
			inventory.AddGear(Longsword());
			smallStats.AddWeaponProficiency("martial");

			var atk = smallStats.Attacks().First();
			Assert.IsNotNull(atk);
			var diceRoll = atk.Damage;
			Assert.AreEqual(3, diceRoll.Modifier);

			//Should convert damage based on size
			Assert.AreEqual(DiceSides.d6, diceRoll.Dice.First().Sides);
			Assert.AreEqual(smallStats.MeleeAttackBonus(), atk.AttackBonus);
		}

		[Test]
		public void RangeAttackBonusHaveAttackBonusButNotDamage() {
			inventory.AddGear(ShortBow());
			smallStats.AddWeaponProficiency("martial");
			var atk = smallStats.Attacks().First();
			Assert.IsNotNull(atk);
			var diceRoll = atk.Damage;
			Assert.AreEqual(0, diceRoll.Modifier);
			Assert.AreEqual(DiceSides.d4, diceRoll.Dice.First().Sides);
			Assert.AreEqual(smallStats.RangeAttackBonus(), atk.AttackBonus);
		}

		[Test]
		public void CanAddWeaponProficiencies() {
			smallStats.AddWeaponProficiency("Shortbow");
			Assert.IsTrue(smallStats.IsProficient(ShortBow()));
		}

		[Test]
		public void CanAddAnArrayOfWeaponProficiencies() {
			smallStats.AddWeaponProficiencies(new string[] {"simple", "martial"});
			Assert.IsTrue(smallStats.IsProficient(Longsword()));	
		}

		[Test]
		public void AttacksWithoutProficiencyAreAtMinus4() {
			inventory.AddGear(Nunchaku());
			var atk = smallStats.Attacks().First();
			Assert.IsNotNull(atk);
			Assert.AreEqual(smallStats.MeleeAttackBonus() + OffenseStats.UnproficientWeaponModifier, atk.AttackBonus);
		}

        [Test]
        public void TracksSpecialAttackAbilities()
        {
            var special = new SpecialAttacks();
            smallStats.ProcessSpecialAbilities(special);
            Assert.Greater(smallStats.OffensiveAbilities.Count(), 0);
        }

		private Weapon Longsword() {
			return new Weapon("Longsword", 0, "1d8", DamageTypes.Slashing, 19, 2, 0, WeaponType.OneHanded, WeaponGroup.HeavyBlades, WeaponTrainingLevel.Martial);
		}

		private Weapon ShortBow() {
			return new Weapon("Shortbow", 0, "1d6", DamageTypes.Piercing, 19, 2, 0, WeaponType.Ranged, WeaponGroup.Bows, WeaponTrainingLevel.Martial);
		}

		private Weapon Nunchaku() {
			return new Weapon("Nunchaku", 0, "1d6", DamageTypes.Bludgeoning, 20, 2, 0, WeaponType.OneHanded, WeaponGroup.Monk, WeaponTrainingLevel.Exotic);
		}

		class MockMod : IModifiesStats {
			public IList<BasicStatModifier> Modifiers { get; set;  }

			public MockMod() {
				Modifiers = new List<BasicStatModifier>();
				Modifiers.Add(new BasicStatModifier("CMD", 1, "racial", "Trait"));
				Modifiers.Add(new BasicStatModifier("CMB", 1, "racial", "Trait"));
			}
		}

        class SpecialAttacks : IProvidesSpecialAbilities 
        {
            public IList<SpecialAbility> SpecialAbilities { get; set; }

            public SpecialAttacks() {
                SpecialAbilities = new List<SpecialAbility>();
                SpecialAbilities.Add(new SpecialAbility("Sneak Attack 1d6", "Offensive"));

            }
        }
	}
}
