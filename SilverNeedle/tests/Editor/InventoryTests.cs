using System.Linq;
using System.IO;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Characters;
using SilverNeedle.Equipment;

namespace Characters {

	[TestFixture]
	public class InventoryTests {
		[Test]
		public void InventoryTracksWeapons() {
			var inv = new Inventory ();
			var wpn1 = new Weapon ();
			var gear = new PieceOfJunk ();
			inv.AddGear (wpn1);
			inv.AddGear (gear);

			Assert.AreEqual (wpn1, inv.Weapons.First ());
		}

		[Test]
		public void InventoryWillReturnCurrentArmor() {
			var inv = new Inventory ();
			var armor = new Armor ();
			inv.AddGear (armor);

			Assert.AreEqual (armor, inv.Armor.First());
		}

		[Test]
		public void InventoryItemsCanBeMarkedAsEquipped() {
			var inv = new Inventory ();
			var armor = new Armor ();
			var armor2 = new Armor ();
			inv.AddGear (armor);
			inv.AddGear (armor2);
			inv.EquipItem (armor);

			Assert.AreEqual (1, inv.EquippedItems.Count());
			Assert.AreEqual (armor, inv.EquippedItems.First ());
		}

		[Test]
		public void CanGetItemOfSpecificType() {
			var inv = new Inventory ();
			var armor = new Armor ();
			var junk = new PieceOfJunk ();
			var wpn = new Weapon ();
			inv.AddGear (armor);
			inv.AddGear (junk);
			inv.AddGear (wpn);

			Assert.AreEqual (junk, inv.GearOfType<PieceOfJunk> ().First());
		}

		[Test]
		public void EquippingItemAddsItToInventoryIfNotAlreadyThere() {
			var inv = new Inventory ();
			var armor = new Armor ();
			inv.EquipItem (armor);
			Assert.AreEqual (armor, inv.All.First ());
		}

		[Test]
		public void AddingTheSameItemTwiceDoesNotDuplicate() {
			var inv = new Inventory ();
			var j = new PieceOfJunk ();
			inv.AddGear (j);
			inv.AddGear (j);
			Assert.AreEqual (1, inv.All.Count ());
		}


		class PieceOfJunk : IEquipment {
			public string Name { get { return "Junk"; } }
			public float Weight { get { return 0.5f; } }
		}
	}
}
