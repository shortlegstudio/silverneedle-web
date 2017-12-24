// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    
    public class EquippedArmorModifierTests
    {
        [Fact]
        public void UnEquippedArmorMakesNoDifference() {
            var bag = new ComponentContainer(); 
            var inv = new Inventory ();
            bag.Add(inv);
            var mod = new EquippedArmorClassModifier(bag);

            var armor = new Armor ();
            armor.ArmorClass = 12;
            inv.AddGear (armor);

            Assert.Equal (0, mod.Modifier);
        }

        [Fact]
        public void EquippedArmorIncreasesYourDefenseAndYourFlatFootedDefenseButNotTouchDefense() {
            var bag = new ComponentContainer();
            var inventory = new Inventory ();
            bag.Add(inventory);
            var mod = new EquippedArmorClassModifier(bag);

            var armor = new Armor ();
            armor.ArmorClass = 10;
            inventory.AddGear (armor);
            inventory.EquipItem (armor);
            Assert.Equal (10, mod.Modifier);
        }
    }
}