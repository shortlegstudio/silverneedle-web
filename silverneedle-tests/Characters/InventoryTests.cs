// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Treasure;

    
    public class InventoryTests {
        [Fact]
        public void InventoryTracksWeapons() {
            var inv = new Inventory ();
            var wpn1 = new Weapon ();
            var gear = new PieceOfJunk ();
            inv.AddGear (wpn1);
            inv.AddGear (gear);

            Assert.Equal (wpn1, inv.Weapons.First ());
        }

        [Fact]
        public void InventoryWillReturnCurrentArmor() {
            var inv = new Inventory ();
            var armor = new Armor ();
            inv.AddGear (armor);

            Assert.Equal (armor, inv.Armor.First());
        }

        [Fact]
        public void InventoryItemsCanBeMarkedAsEquipped() {
            var inv = new Inventory ();
            var armor = new Armor ();
            inv.EquipItem (armor);
            Assert.Equal (1, inv.EquippedItems.Count());
        }

        [Fact]
        public void CanGetEquippedArmor()
        {
            var inv = new Inventory();
            var armor = new Armor();
            inv.EquipItem(armor);
            Assert.NotStrictEqual(inv.Equipped<IArmor>(), new Armor[] { armor });
        }

        [Fact]
        public void CanGetItemOfSpecificType() {
            var inv = new Inventory ();
            var armor = new Armor ();
            var junk = new PieceOfJunk ();
            var wpn = new Weapon ();
            inv.AddGear (armor);
            inv.AddGear (junk);
            inv.AddGear (wpn);

            Assert.Equal (junk, inv.GearOfType<PieceOfJunk> ().First());
        }

        [Fact]
        public void EquippingItemAddsItToInventoryIfNotAlreadyThere() {
            var inv = new Inventory ();
            var armor = new Armor ();
            inv.EquipItem (armor);
            Assert.Equal (armor, inv.Armor.First ());
        }

        [Fact]
        public void AddingTheSameItemTwiceIncrementsQuantity() {
            var inv = new Inventory ();
            var j = new PieceOfJunk ();
            inv.AddGear (j);
            inv.AddGear (j);
            var item = inv.Find(j);
            Assert.Equal(item.Quantity, 2);
        }

        [Fact]
        public void PurchasingAnItemDeductsTheValueFromPurse()
        {
            var inv = new Inventory();
            var j = new PieceOfJunk();
            j.Value = 34;
            inv.CoinPurse.SetValue(40);
            inv.Purchase(j);
            Assert.Equal(6, inv.CoinPurse.Value);
            Assert.Equal(1, inv.All.Count());
        }

        [Fact]
        public void ProvideMethodForGettingEquippedItemsOfType()
        {
            var inv = new Inventory();
            inv.EquipItem(new Armor());
            inv.EquipItem(new Weapon());
            inv.EquipItem(new Weapon());

            var equippedArmors = inv.Equipped<Armor>();
            Assert.Equal(equippedArmors.Count(), 1);
        }

        [Fact]
        public void ToStringArrayCreatesAnArrayOfTheNameAndQuantityIfAppropriate()
        {
            var inv = new Inventory();
            var armor = new Armor();
            armor.Name = "Leather Armor";
            inv.AddGear(armor);
            var junk = new PieceOfJunk();
            junk.GroupSimilar = true;
            inv.AddGear(junk);
            inv.AddGear(junk);

            string[] names = inv.ToStringArray();
            Assert.Single(names, "Leather Armor");
            Assert.Single(names, "Junk (2)");
        }

        [Fact]
        public void CanReturnSpellbooksInInventory()
        {
            var inv = new Inventory();
            var spells = new Spellbook();
            inv.AddGear(spells);
            Assert.NotStrictEqual(inv.Spellbooks, new Spellbook[] { spells });
        }

        [Fact]
        public void RaiseAnEventWhenItemsAreAddedToInventory()
        {
            var inv = new Inventory();
            var gear = new Armor();
            var called = false;
            inv.ItemAdded += delegate(object o, InventoryEventArgs args) {
                called = args.Item == gear;
            };
            inv.AddGear(gear);
            Assert.True(called);
        }

        [Fact]
        public void ItemsInInventoryShouldBeModifiableWithCharacterStatAdjustments()
        {
            var inv = new Inventory();
            inv.ItemAdded += delegate(object o, InventoryEventArgs args)
            {
                if(args.GearType == typeof(Armor))
                {
                    var armor = args.Item as Armor;
                    armor.ArmorCheckPenalty = 4;
                }
            };
            var gear = new Armor();
            gear.ArmorCheckPenalty = 5;
            inv.AddGear(gear);

            var item = inv.GearOfType<Armor>().First();
            Assert.Equal(item.ArmorCheckPenalty, 4);
        }

        class PieceOfJunk : IGear {
            public string Name { get { return "Junk"; } }
            public float Weight { get { return 0.5f; } }

            public int Value { get; set; }

            public bool GroupSimilar { get; set; }
        }
    }
}
