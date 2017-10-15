// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
    using Xunit;
    using System.Linq;
    using System.Collections.Generic;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Shops;
    
    
    public class PurchaseRangedWeaponTests 
    {
        WeaponShop shop;
        CharacterSheet character;
        PurchaseRangedWeapon subject;


        public PurchaseRangedWeaponTests()
        {
            var weapons = new List<Weapon> ();
            var wpn1 = new Weapon ("Mace", 0f, "1d6", 
                DamageTypes.Bludgeoning, 20, 2, 0, 
                WeaponType.OneHanded, WeaponGroup.Hammers, 
                WeaponTrainingLevel.Simple);
            var wpn2 = new Weapon ("Bow", 0, "1d6", 
                DamageTypes.Piercing, 20, 2, 0, 
                WeaponType.Ranged, WeaponGroup.Bows, 
                WeaponTrainingLevel.Martial);
            wpn2.Value = 3000;
            var wpn3 = new Weapon ("Never Pick", 0, "1d6", 
                DamageTypes.Piercing, 20, 2, 0, 
                WeaponType.Ranged, WeaponGroup.Bows, 
                WeaponTrainingLevel.Exotic);
            weapons.Add (wpn1);
            weapons.Add (wpn2);
            weapons.Add (wpn3);
            shop = new WeaponShop(weapons);

            subject = new PurchaseRangedWeapon (shop);

            var proficiencies = new string[] { "simple", "martial" };
            character = new CharacterSheet();                
            character.Inventory.CoinPurse.SetValue(30000);
            character.Offense.AddWeaponProficiencies(proficiencies);
        }

        [Theory]
        [Repeat(1000)]
        public void CharactersGetARangedAndMeleeWeaponTheyAreProficientIn() {
            //Bad test, but good enough for now
            var action = new PurchaseRangedWeapon (shop);
            var proficiencies = new string[] { "simple", "martial" };
            var character = new CharacterSheet();
            character.Inventory.CoinPurse.SetValue(30000);
                
            character.Offense.AddWeaponProficiencies(proficiencies);
            action.ExecuteStep(character, new CharacterStrategy());
            Assert.Equal(character.Inventory.Weapons.Count (), 1);
            Assert.True(character.Inventory.Weapons.Any (x => x.Type == WeaponType.Ranged));
            Assert.False(character.Inventory.Weapons.Any (x => x.Type != WeaponType.Ranged));
            Assert.False(character.Inventory.Weapons.Any(x => x.Level == WeaponTrainingLevel.Exotic));
        }

        [Fact]
        public void IfNoAppropriateItemsAreFoundAssignNothing()
        {
            var action = new PurchaseRangedWeapon (shop);
            var character = new CharacterSheet();
            character.Inventory.CoinPurse.SetValue(30000);
            //With no specification nothing should match
            action.ExecuteStep(character, new CharacterStrategy());
            Assert.Empty(character.Inventory.Weapons);
        }

         [Fact]
        public void AvoidTryingToBuyWeaponsIfBroke()
        {
            character.Inventory.CoinPurse.SetValue(2999); // Not Enough :'(
            subject.ExecuteStep(character, new CharacterStrategy());
            Assert.Equal(2999, character.Inventory.CoinPurse.Value);
            Assert.Equal(0, character.Inventory.Weapons.Count());
        }

        [Fact]
        public void PurchasingARangedWeaponSpendsMoney()
        {
            subject.ExecuteStep(character, new CharacterStrategy());
            Assert.Equal(27000, character.Inventory.CoinPurse.Value);
        }

    }
}