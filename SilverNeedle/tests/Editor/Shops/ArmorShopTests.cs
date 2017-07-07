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
    public class ArmorShopTests
    {
        [Test]
        public void ProvidesACollectionOfArmor()
        {
            var shop = new ArmorShop();
            var armors = shop.GetInventory<IArmor>();
            Assert.That(armors, Is.Not.Empty);
        }

        [Test]
        public void ProvidesSomeMasterworkArmor()
        {
            var shop = new ArmorShop();
            var armors = shop.GetInventory();
            var mwkArmors = shop.GetInventory<MasterworkArmor>();
            Assert.That(armors, Is.Not.Empty);
            Assert.That(mwkArmors, Is.Not.Empty);
        }
    }
}