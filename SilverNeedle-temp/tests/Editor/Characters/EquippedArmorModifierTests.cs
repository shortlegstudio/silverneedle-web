// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    [TestFixture]
    public class EquippedArmorModifierTests
    {
        [Test]
        public void UnEquippedArmorMakesNoDifference() {
            var bag = new ComponentBag(); 
            var inv = new Inventory ();
            bag.Add(inv);
            var mod = new EquippedArmorClassModifier(bag);

            var armor = new Armor ();
            armor.ArmorClass = 12;
            inv.AddGear (armor);

            Assert.AreEqual (0, mod.Modifier);
        }

        [Test]
        public void EquippedArmorIncreasesYourDefenseAndYourFlatFootedDefenseButNotTouchDefense() {
            var bag = new ComponentBag();
            var inventory = new Inventory ();
            bag.Add(inventory);
            var mod = new EquippedArmorClassModifier(bag);

            var armor = new Armor ();
            armor.ArmorClass = 10;
            inventory.AddGear (armor);
            inventory.EquipItem (armor);
            Assert.AreEqual (10, mod.Modifier);
        }
    }
}