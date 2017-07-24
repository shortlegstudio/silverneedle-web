// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Shops
{
    using NUnit.Framework;
    using SilverNeedle.Equipment;
    using SilverNeedle.Shops;

    [TestFixture]
    public class WeaponShopTests
    {
        [Test]
        public void ProvidesWeapons()
        {
            var shop = new WeaponShop();
            var weapons = shop.GetInventory<IWeapon>();
            Assert.That(weapons, Is.Not.Empty);
        }

        [Test]
        public void ProvidesMasterworkVersionsOfWeapons()
        {
            var shop = new WeaponShop();
            var masterwork = shop.GetInventory<MasterworkWeapon>();
            Assert.That(masterwork, Is.Not.Empty);
        }

        [Test]
        public void AddSomeMagicWeaponsIntoTheMix()
        {
            var shop = new WeaponShop();
            var magical = shop.GetInventory<MagicWeapon>();
            Assert.That(magical, Is.Not.Empty);
        }
    }

}