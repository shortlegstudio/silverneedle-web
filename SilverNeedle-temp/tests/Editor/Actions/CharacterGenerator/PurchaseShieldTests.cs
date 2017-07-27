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
	using SilverNeedle.Serialization;

	[TestFixture]
	public class PurchaseShieldTests
	{
		EntityGateway<Armor> gateway;

        CharacterSheet proficientCharacter;

        CharacterSheet incapableCharacter;        

        PurchaseShield subject;
        Armor shield;

		[SetUp]
		public void SetUp()
		{
			var armors = new List<Armor>();
			shield = new Armor();
			shield.ArmorType = ArmorType.Shield;

			armors.Add(shield);
			gateway = new EntityGateway<Armor>(armors);
            proficientCharacter = new CharacterSheet();
            proficientCharacter.Defense.AddArmorProficiencies(new string[] {"shield"});
            incapableCharacter = new CharacterSheet();

            subject = new PurchaseShield(gateway);
		}

		[Fact]
		public void PurchaseAShield () {
			subject.Process (proficientCharacter, new CharacterBuildStrategy());

			Assert.That (proficientCharacter.Inventory.Armor, Has.Exactly(1).EqualTo(shield));
		}

        [Fact]
        public void DoesNotEquipShieldIfNotProficient()
        {
            subject.Process(incapableCharacter, new CharacterBuildStrategy());
            Assert.That(incapableCharacter.Inventory.Armor, Has.Exactly(0).EqualTo(shield));
        }

        [Fact]
        public void PurchasingArmorCostsMoney()
        {
            shield.Value = 3700;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.Process(proficientCharacter, new CharacterBuildStrategy());

            Assert.That(proficientCharacter.Inventory.CoinPurse.Value, Is.EqualTo(300));
            Assert.That(proficientCharacter.Inventory.Armor, Has.Exactly(1).EqualTo(shield));
        }

        [Fact]
        public void DoNotPurchaseTooExpensiveItems()
        {
            shield.Value = 60000;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.Process(proficientCharacter, new CharacterBuildStrategy());

            Assert.That(proficientCharacter.Inventory.CoinPurse.Value, Is.EqualTo(4000));
            Assert.That(proficientCharacter.Inventory.Armor, Has.Exactly(0).EqualTo(shield));

        }
    }
}

