// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Shops
{
    using Xunit;
    using SilverNeedle.Equipment;
    using SilverNeedle.Shops;

    public class WeaponShopTests : RequiresDataFiles
    {
        [Fact]
        public void ProvidesWeapons()
        {
            var shop = new WeaponShop();
            var weapons = shop.GetInventory<IWeapon>();
            Assert.NotEmpty(weapons);
        }

        [Fact]
        public void ProvidesMasterworkVersionsOfWeapons()
        {
            var shop = new WeaponShop();
            var masterwork = shop.GetInventory<MasterworkWeapon>();
            Assert.NotEmpty(masterwork);
        }

        [Fact]
        public void AddSomeMagicWeaponsIntoTheMix()
        {
            var shop = new WeaponShop();
            var magical = shop.GetInventory<MagicWeapon>();
            Assert.NotEmpty(magical);
        }
    }

}