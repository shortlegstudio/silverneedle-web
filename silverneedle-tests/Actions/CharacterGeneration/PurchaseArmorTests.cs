// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Equipment;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Shops;

    
    public class PurchaseArmorTests
    {
        ArmorShop armorShop;

        CharacterSheet proficientCharacter;

        CharacterSheet incapableCharacter;        

        PurchaseArmor subject;
        Armor plate;

        public PurchaseArmorTests()
        {
            var armors = new List<Armor>();
            plate = new Armor();
            plate.ArmorType = ArmorType.Heavy;
            plate.Name = "Full Plate";
            armors.Add(plate);
            
            armorShop = new ArmorShop(armors);
            proficientCharacter = CharacterTestTemplates.AverageBob();
            proficientCharacter.Defense.AddArmorProficiencies(new string[] {"light", "medium", "heavy", "shield"});
            incapableCharacter = CharacterTestTemplates.AverageBob();

            subject = new PurchaseArmor(armorShop);
        }

        [Fact]
        public void EquipWithArmorIfProficient () {
            subject.ExecuteStep (proficientCharacter);

            Assert.Single (proficientCharacter.Inventory.Armor, plate);
        }

        [Fact]
        public void DoesNotEquipArmorIfNotProficient()
        {
            subject.ExecuteStep (incapableCharacter);
            Assert.True (incapableCharacter.Inventory.Armor.None(x => x.Equals(plate)));
        }

        [Fact]
        public void PurchasingArmorCostsMoney()
        {
            plate.Value = 3700;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.ExecuteStep(proficientCharacter);

            Assert.Equal(proficientCharacter.Inventory.CoinPurse.Value, 300);
            Assert.Single(proficientCharacter.Inventory.Armor, plate);
        }

        [Fact]
        public void DoNotPurchaseTooExpensiveItems()
        {
            plate.Value = 60000;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.ExecuteStep(proficientCharacter);

            Assert.Equal(proficientCharacter.Inventory.CoinPurse.Value, 4000);
            Assert.True(proficientCharacter.Inventory.Armor.None(x => x == plate));

        }

        [Fact]
        public void QuantityShouldBeOne()
        {
            subject.ExecuteStep (proficientCharacter);
            Assert.Single (proficientCharacter.Inventory.Armor, plate);
            var armorPossession = proficientCharacter.Inventory.Find(plate);
            Assert.Equal(armorPossession.Quantity, 1);
        }
    }
}

