// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.EquipCharacter;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class PurchaseClothingTests
    {
        [Fact]
        public void SelectsClothingFromPossiblePurchasePoints()
        {
            var clothes = new Clothes("Outfit", 2000, 5);
            var purchase = new PurchaseClothing(EntityGateway<Clothes>.LoadWithSingleItem(clothes));
            var bob = CharacterTestTemplates.AverageBob();
            bob.Inventory.CoinPurse.AddGold(200);

            purchase.ExecuteStep(bob, new CharacterBuildStrategy());
            Assert.NotNull(bob.Inventory.Find(clothes));
        }

        [Fact]
        public void DonotBuyThingsYouCannotAfford()
        {
            var clothes = new Clothes("Outfit", 2000, 5);
            var purchase = new PurchaseClothing(EntityGateway<Clothes>.LoadWithSingleItem(clothes));
            var bob = CharacterTestTemplates.AverageBob();
            bob.Inventory.CoinPurse.AddGold(1);

            purchase.ExecuteStep(bob, new CharacterBuildStrategy());
            Assert.Null(bob.Inventory.Find(clothes));
        }
    }
}