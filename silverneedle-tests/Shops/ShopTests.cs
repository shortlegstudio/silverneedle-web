// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Shops
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Equipment;
    using SilverNeedle.Shops;

    public class ShopTests
    {
        [Fact]
        public void PurchasingAnItemRemovesItFromInventory()
        {
            var gear = new List<IGear>();
            var mugICanAffor = new Gear("Coffee Mug", 10, 1); 
            var fonduPotICannot = new Gear("Fondu Pot", 4000, 102);
            gear.Add(mugICanAffor);
            gear.Add(fonduPotICannot);
            var shop = new Shop(gear);

            var affordableItems = shop.GetAffordableInventory(3000);
            Assert.Contains(mugICanAffor, affordableItems);
            Assert.DoesNotContain(fonduPotICannot, affordableItems);
        }

        [Fact]
        public void SellItemRemovesItFromInventory()
        {
            var gear = new List<IGear>();
            var mugICanAffor = new Gear("Coffee Mug", 10, 1); 
            gear.Add(mugICanAffor);

            var shop = new Shop(gear);
            shop.SellItem(mugICanAffor);
            Assert.DoesNotContain(mugICanAffor, shop.GetInventory());
        }
    }
}