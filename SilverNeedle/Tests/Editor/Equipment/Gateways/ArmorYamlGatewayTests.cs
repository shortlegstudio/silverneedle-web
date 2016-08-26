using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle.Equipment;
using SilverNeedle.Equipment.Gateways;
using SilverNeedle;
using SilverNeedle.Characters;
using SilverNeedle.Yaml;

namespace Equipment {
	[TestFixture]
	public class ArmorYamlGatewayTests
	{
		ArmorYamlGateway gateway;

		[SetUp]
		public void ReadArmorYamlString() {
			gateway = new ArmorYamlGateway (ArmorYamlFile.ParseYaml ());
		}
			
		[Test]
		public void YouCanGetAllTheArmors() {
			var armors = gateway.All ();
			Assert.AreEqual (3, armors.Count ());
		}

		[Test]
		public void YouCanAccessASpecificSetOfArmor() {
			var leather = gateway.GetByName ("Leather Armor");
			Assert.IsNotNull (leather);
			Assert.AreEqual ("Leather Armor", leather.Name);

			var plate = gateway.GetByName ("Full Plate");
			Assert.IsNotNull (plate);
			Assert.AreEqual ("Full Plate", plate.Name);
		}

		[Test]
		public void ArmorLoadsItsArmorClass() {
			var leather = gateway.GetByName ("Leather Armor");
			Assert.AreEqual (2, leather.ArmorClass);
			var plate = gateway.GetByName ("Full Plate");
			Assert.AreEqual (9, plate.ArmorClass);
		}

		[Test]
		public void ArmorHasWeight() {
			var leather = gateway.GetByName ("Leather Armor");
			Assert.AreEqual (15, leather.Weight);
		}

		[Test]
		public void ArmorHasMaxDexBonus() {
			var plate = gateway.GetByName ("Full Plate");
			Assert.AreEqual (1, plate.MaximumDexterityBonus);
		}	

		[Test]
		public void ArmorHasArcaneSpellFailure() {
			var leather = gateway.GetByName ("Leather Armor");
			Assert.AreEqual (10, leather.ArcaneSpellFailureChance);
		}

		[Test]
		public void ArmorHasACheckPenalty() {
			var plate = gateway.GetByName ("Full Plate");
			Assert.AreEqual (-6, plate.ArmorCheckPenalty);
		}

		[Test]
		public void ArmorHasAType() {
			var plate = gateway.GetByName ("Full Plate");
			Assert.AreEqual (ArmorType.Heavy, plate.ArmorType);
		}

		[Test]
		public void GetAllArmorsOfAType() {
			var armors = gateway.FindByArmorType (ArmorType.Heavy);
			Assert.AreEqual (2, armors.Count ());
			Assert.IsTrue (armors.All (x => x.ArmorType == ArmorType.Heavy));
		}

		[Test]
		public void GetArmorsOfTypes() {
			var armors = gateway.FindByArmorTypes (ArmorType.Light, ArmorType.Heavy);
			Assert.AreEqual (3, armors.Count ());
		}

        [Test]
        public void GetArmorsByProficiencies() {
            var proficiencies = new List<ArmorProficiency>();
            proficiencies.Add(new ArmorProficiency("Light"));
            var armors = gateway.FindByProficiency(proficiencies);
            Assert.AreEqual(1, armors.Count());
        }
		const string ArmorYamlFile = @"
- armor:
  name: Leather Armor
  armor_class: 2
  weight: 15
  maximum_dexterity_bonus: 6
  armor_check_penalty: 0
  arcane_spell_failure_chance: 10
  armor_type: Light
- armor:
  name: Full Plate
  armor_class: 9
  weight: 50
  maximum_dexterity_bonus: 1
  armor_check_penalty: -6
  arcane_spell_failure_chance: 35
  armor_type: Heavy
- armor:
  name: Half Plate
  armor_class: 8
  weight: 50
  maximum_dexterity_bonus: 0
  armor_check_penalty: -7
  arcane_spell_failure_chance: 40
  armor_type: Heavy
";
	}
}
