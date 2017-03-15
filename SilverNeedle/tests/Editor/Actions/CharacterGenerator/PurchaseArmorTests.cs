// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
	using System.Linq;
	using System.Collections.Generic;
	using NUnit.Framework;
	using SilverNeedle.Actions.CharacterGenerator;
	using SilverNeedle.Equipment;
	using SilverNeedle.Characters;
	using SilverNeedle.Utility;

	[TestFixture]
	public class PurchaseArmorTests
	{
		EntityGateway<Armor> gateway;

        CharacterSheet proficientCharacter;

        CharacterSheet incapableCharacter;        

        PurchaseArmor subject;
        Armor plate;

		[SetUp]
		public void SetUp()
		{
			var armors = new List<Armor>();
			plate = new Armor();
			plate.ArmorType = ArmorType.Heavy;
            plate.Name = "Full Plate";
			armors.Add(plate);
			
            gateway = new EntityGateway<Armor>(armors);
            proficientCharacter = new CharacterSheet();
            proficientCharacter.Defense.AddArmorProficiencies(new string[] {"light", "medium", "heavy", "shield"});
            incapableCharacter = new CharacterSheet();

            subject = new PurchaseArmor(gateway);
		}

		[Test]
		public void EquipWithArmorIfProficient () {
			subject.Process (proficientCharacter, new CharacterBuildStrategy());

			Assert.That (proficientCharacter.Inventory.Armor, Has.Exactly(1).EqualTo(plate));
		}

        [Test]
        public void DoesNotEquipArmorIfNotProficient()
        {
            subject.Process (incapableCharacter, new CharacterBuildStrategy());
            Assert.That (incapableCharacter.Inventory.Armor, Has.Exactly(0).EqualTo(plate));
        }

        [Test]
        public void PurchasingArmorCostsMoney()
        {
            plate.Value = 3700;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.Process(proficientCharacter, new CharacterBuildStrategy());

            Assert.That(proficientCharacter.Inventory.CoinPurse.Value, Is.EqualTo(300));
            Assert.That(proficientCharacter.Inventory.Armor, Has.Exactly(1).EqualTo(plate));
        }

        [Test]
        public void DoNotPurchaseTooExpensiveItems()
        {
            plate.Value = 60000;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.Process(proficientCharacter, new CharacterBuildStrategy());

            Assert.That(proficientCharacter.Inventory.CoinPurse.Value, Is.EqualTo(4000));
            Assert.That(proficientCharacter.Inventory.Armor, Has.Exactly(0).EqualTo(plate));

        }
    }
}

