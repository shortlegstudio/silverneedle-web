// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Shops
{
    using Xunit;
    using SilverNeedle.Shops;
    using SilverNeedle.Equipment;

    public class MagicShopTests : RequiresDataFiles
    {
        [Fact]
        public void StocksSomeWands()
        {
            var shop = new MagicShop();

            var wands = shop.GetInventory<IWand>();
            Assert.NotEmpty(wands);
        }
    }
}