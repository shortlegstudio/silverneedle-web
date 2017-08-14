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
        private MagicShop magicShop;
        public MagicShopTests()
        {
            magicShop = new MagicShop();
        }

        [Fact]
        public void StocksSomeWands()
        {
            var wands = magicShop.GetInventory<IWand>();
            Assert.NotEmpty(wands);
        }

        [Fact]
        public void StockSomePotions()
        {
            var potions = magicShop.GetInventory<IPotion>();
            Assert.NotEmpty(potions);
        }
    }
}