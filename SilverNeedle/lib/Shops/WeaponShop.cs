// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Shops
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;
    public class WeaponShop : Shop
    {
        public WeaponShop()
        {
            StockShop();
        }

        public WeaponShop(IEnumerable<IGear> inventory)
        {
            StockShop(inventory);
        }

        private void StockShop()
        {
            var weapons = GatewayProvider.All<Weapon>();
            var masterwork = weapons.Select(x => new MasterworkWeapon(x));
            var magical = weapons.Select(x => new MagicWeapon(x, Randomly.Range(1,6)));
            var total = weapons.Concat<IWeapon>(masterwork).Concat<IWeapon>(magical);
            StockShop(total);
        }

        private void StockShop(IEnumerable<IGear> items)
        {
            inventory.Add(items);
        }
    }
}