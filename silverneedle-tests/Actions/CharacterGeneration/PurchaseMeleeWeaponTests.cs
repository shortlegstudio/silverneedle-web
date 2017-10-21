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
    
    
    public class PurchaseMeleeWeaponTests 
    {
        WeaponShop shop;
        CharacterSheet character;
        PurchaseMeleeWeapon subject;

        public PurchaseMeleeWeaponTests()
        {
            var weapons = new List<Weapon> ();
            var wpn1 = new Weapon ("Mace", 0f, "1d6", 
                DamageTypes.Bludgeoning, 20, 2, 0, 
                WeaponType.OneHanded, WeaponGroup.Hammers, 
                WeaponTrainingLevel.Simple);
            wpn1.Value = 3000;
            var wpn2 = new Weapon ("Bow", 0, "1d6", 
                DamageTypes.Piercing, 20, 2, 0, 
                WeaponType.Ranged, WeaponGroup.Bows, 
                WeaponTrainingLevel.Martial);
            var wpn3 = new Weapon ("Never Pick", 0, "1d6", 
                DamageTypes.Piercing, 20, 2, 0, 
                WeaponType.Ranged, WeaponGroup.Bows, 
                WeaponTrainingLevel.Exotic);
            weapons.Add (wpn1);
            weapons.Add (wpn2);
            weapons.Add (wpn3);
            shop = new WeaponShop(weapons);
            subject = new PurchaseMeleeWeapon (shop);

            var proficiencies = new string[] { "simple" };
            character = new CharacterSheet(CharacterStrategy.Default());                
            character.Inventory.CoinPurse.SetValue(30000);
            character.Offense.AddWeaponProficiencies(proficiencies);


        }

        [Theory]
        [Repeat(1000)]
        public void CharactersGetAMeleeWeaponIfTheyAreProficient() {
            //Bad test, but good enough for now
            var action = new PurchaseMeleeWeapon (shop);
            var proficiencies = new string[] { "simple", "martial" };
            var character = new CharacterSheet(CharacterStrategy.Default());                
            character.Inventory.CoinPurse.SetValue(30000);
            character.Offense.AddWeaponProficiencies(proficiencies);

            action.ExecuteStep(character);
            Assert.Equal(character.Inventory.Weapons.Count (), 1);
            Assert.True(character.Inventory.Weapons.Any (x => x.Type != WeaponType.Ranged));
            Assert.False(character.Inventory.Weapons.Any (x => x.Type == WeaponType.Ranged));
            Assert.False(character.Inventory.Weapons.Any(x => x.Level == WeaponTrainingLevel.Exotic));
        }

        [Fact]
        public void PurchasingAMeleeWeaponSpendsMoney()
        {
            character.Inventory.CoinPurse.SetValue(30000);
            subject.ExecuteStep(character);
            Assert.Equal(27000, character.Inventory.CoinPurse.Value);
        }

        [Fact]
        public void IfNoAppropriateItemsAreFoundAssignNothing()
        {
            var action = new PurchaseMeleeWeapon (shop);
            var character = new CharacterSheet(CharacterStrategy.Default());
            //With no specification nothing should match
            action.ExecuteStep(character);
            Assert.Empty(character.Inventory.Weapons);
        }

        [Fact]
        public void AvoidTryingToBuyWeaponsIfBroke()
        {
            character.Inventory.CoinPurse.SetValue(2999); // Not Enough :'(
            subject.ExecuteStep(character);
            Assert.Equal(2999, character.Inventory.CoinPurse.Value);
            Assert.Equal(0, character.Inventory.Weapons.Count());
        }
    }
}