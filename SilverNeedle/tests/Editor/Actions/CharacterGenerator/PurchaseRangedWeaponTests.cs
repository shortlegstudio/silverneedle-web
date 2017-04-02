// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
    using NUnit.Framework;
    using System.Linq;
    using System.Collections.Generic;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    
	[TestFixture]
	public class PurchaseRangedWeaponTests 
    {
        EntityGateway<Weapon> gateway;
        CharacterSheet character;
        PurchaseRangedWeapon subject;


        [SetUp]
        public void SetUp()
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
            var wpn3 = new Weapon ("Never Pick", 0, "1d6", 
                DamageTypes.Piercing, 20, 2, 0, 
                WeaponType.Ranged, WeaponGroup.Bows, 
                WeaponTrainingLevel.Exotic);
            weapons.Add (wpn1);
            weapons.Add (wpn2);
            weapons.Add (wpn3);
            gateway = new EntityGateway<Weapon>(weapons);

            subject = new PurchaseRangedWeapon (gateway);

            var proficiencies = new string[] { "simple", "martial" };
            character = new CharacterSheet();                
            character.Offense.AddWeaponProficiencies(proficiencies);
        }

		[Test]
		public void CharactersGetARangedAndMeleeWeaponTheyAreProficientIn() {
			//Bad test, but good enough for now
			for (int i = 0; i < 1000; i++) {
                var action = new PurchaseRangedWeapon (gateway);
				var proficiencies = new string[] { "simple", "martial" };
                var character = new CharacterSheet();
                
                character.Offense.AddWeaponProficiencies(proficiencies);
                action.Process(character, new CharacterBuildStrategy());
				Assert.AreEqual(character.Inventory.Weapons.Count (), 1);
				Assert.IsTrue(character.Inventory.Weapons.Any (x => x.Type == WeaponType.Ranged));
				Assert.IsFalse(character.Inventory.Weapons.Any (x => x.Type != WeaponType.Ranged));
				Assert.IsFalse(character.Inventory.Weapons.Any(x => x.Level == WeaponTrainingLevel.Exotic));
			}
		}

        [Test]
        public void IfNoAppropriateItemsAreFoundAssignNothing()
        {
            var action = new PurchaseRangedWeapon (gateway);
            var character = new CharacterSheet();
            //With no specification nothing should match
			action.Process(character, new CharacterBuildStrategy());
            Assert.IsEmpty(character.Inventory.Weapons);
        }

         [Test]
        public void AvoidTryingToBuyWeaponsIfBroke()
        {
            var bow = gateway.Find("Bow");
            bow.Value = 3000;
            character.Inventory.CoinPurse.SetValue(2999); // Not Enough :'(
            subject.Process(character, new CharacterBuildStrategy());
            Assert.AreEqual(2999, character.Inventory.CoinPurse.Value);
            Assert.That(character.Inventory.Weapons, Has.Exactly(0).Count);
        }

        [Test]
        public void PurchasingARangedWeaponSpendsMoney()
        {
            var bow = gateway.Find("bow");
            bow.Value = 3000;
            character.Inventory.CoinPurse.SetValue(30000);
            subject.Process(character, new CharacterBuildStrategy());
            Assert.AreEqual(27000, character.Inventory.CoinPurse.Value);
        }

	}
}