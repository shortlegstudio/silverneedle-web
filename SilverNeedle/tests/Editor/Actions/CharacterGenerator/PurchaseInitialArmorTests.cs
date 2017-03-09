using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Equipment;
using SilverNeedle.Characters;
using SilverNeedle.Utility;


namespace Actions {

	[TestFixture]
	public class PurchaseInitialArmorTests
	{
		EntityGateway<Armor> gateway;

		[SetUp]
		public void SetUp()
		{
			var armors = new List<Armor>();
			var shield = new Armor();
			shield.ArmorType = ArmorType.Shield;

			var armor = new Armor();
			armor.ArmorType = ArmorType.Heavy;
			armors.Add(armor);
			armors.Add(shield);
			gateway = new EntityGateway<Armor>(armors);
		}

		[Test]
		public void EquipWithArmorAndShield () {
			var equip = new PurchaseInitialArmor(gateway);
			var inventory = new Inventory ();
            var armorProficiencies = new List<ArmorProficiency>();
            armorProficiencies.Add(new ArmorProficiency("Heavy"));
            armorProficiencies.Add(new ArmorProficiency("Shield"));
			equip.PurchaseArmorAndShield (inventory, armorProficiencies);

			Assert.IsTrue (inventory.GearOfType<Armor> ().Any (x => x.ArmorType == ArmorType.Shield));
			Assert.IsTrue (inventory.GearOfType<Armor> ().Any (x => x.ArmorType != ArmorType.Shield));
		}

        [Test]
        public void DoesNotEquipShieldIfNotProficient()
        {
            var equip = new PurchaseInitialArmor(gateway);
            var inventory = new Inventory();
            var armorProficiencies = new List<ArmorProficiency>();
            armorProficiencies.Add(new ArmorProficiency("Heavy"));
            equip.PurchaseArmorAndShield(inventory, armorProficiencies);
            Assert.IsTrue(inventory.GearOfType<Armor>().Count() > 0);
            Assert.IsFalse(inventory.GearOfType<Armor>().Any(x => x.ArmorType == ArmorType.Shield));
        }

        [Test]
        public void DoesNotEquipArmorIfNotProficient()
        {
            var equip = new PurchaseInitialArmor(gateway);
            var inventory = new Inventory();
            var armorProficiencies = new List<ArmorProficiency>();
            equip.PurchaseArmorAndShield(inventory, armorProficiencies);
            Assert.IsTrue(inventory.GearOfType<Armor>().Count() == 0);
        }
	}
}

