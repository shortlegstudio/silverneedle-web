// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Shops
{
    using Xunit;
    using SilverNeedle.Equipment;
    using SilverNeedle.Shops;

    public class ArmorShopTests : RequiresDataFiles
    {
        [Fact]
        public void ProvidesACollectionOfArmor()
        {
            var shop = new ArmorShop();
            var armors = shop.GetInventory<IArmor>();
            Assert.NotEmpty(armors);
        }

        [Fact]
        public void ProvidesSomeMasterworkArmor()
        {
            var shop = new ArmorShop();
            var armors = shop.GetInventory();
            var mwkArmors = shop.GetInventory<MasterworkArmor>();
            Assert.NotEmpty(armors);
            Assert.NotEmpty(mwkArmors);
        }
    }
}