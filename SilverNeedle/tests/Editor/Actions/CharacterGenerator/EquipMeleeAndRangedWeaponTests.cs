

namespace Tests.Actions {
    using NUnit.Framework;
    using System.Linq;
    using System.Collections.Generic;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    
	[TestFixture]
	public class EquipMeleeAndRangedWeaponTests 
    {
        EntityGateway<Weapon> gateway;

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
        }

		[Test]
		public void CharactersGetARangedAndMeleeWeaponTheyAreProficientIn() {
			//Bad test, but good enough for now
			for (int i = 0; i < 1000; i++) {
				var inventory = new Inventory ();
				var repo = new EquipMeleeAndRangedWeapon (gateway);
				var proficiencies = new WeaponProficiency[] { new WeaponProficiency("simple"), new WeaponProficiency("martial") };
				repo.AssignWeapons (inventory, proficiencies);
				Assert.AreEqual (inventory.Weapons.Count (), 2);
				Assert.IsTrue (inventory.Weapons.Any (x => x.Type == WeaponType.Ranged));
				Assert.IsTrue (inventory.Weapons.Any (x => x.Type != WeaponType.Ranged));
				Assert.IsFalse(inventory.Weapons.Any(x => x.Level == WeaponTrainingLevel.Exotic));
			}
		}
	}
}