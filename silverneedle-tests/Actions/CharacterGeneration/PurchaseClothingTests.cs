// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.EquipCharacter;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class PurchaseClothingTests
    {
        [Fact]
        [Repeat(50)]
        public void SelectsClothingFromOptionsInStrategy()
        {
            
            var clothes = new List<Clothes>() {
                new Clothes("Outfit", 2000, 5),
                new Clothes("Ignore", 200, 5)
            };
            var purchase = new PurchaseClothing(EntityGateway<Clothes>.LoadFromList(clothes));
            var bob = CharacterTestTemplates.AverageBob();
            bob.Inventory.CoinPurse.AddGold(200);
            var strategy = new CharacterStrategy();
            strategy.AddCustomValue("clothes", "Outfit", 1);

            purchase.ExecuteStep(bob, strategy);
            Assert.NotNull(bob.Inventory.Find(clothes[0]));
        }

        [Fact]
        public void DonotBuyThingsYouCannotAfford()
        {
            var clothes = new Clothes("Outfit", 2000, 5);
            var purchase = new PurchaseClothing(EntityGateway<Clothes>.LoadWithSingleItem(clothes));
            var bob = CharacterTestTemplates.AverageBob();
            bob.Inventory.CoinPurse.AddGold(1);
            var strategy = new CharacterStrategy();

            purchase.ExecuteStep(bob, strategy);
            Assert.Null(bob.Inventory.Find(clothes));
        }
    }
}