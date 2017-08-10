// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Shops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    public class ArmorShop : Shop
    {
        public ArmorShop()
        {
            StockShop();
        }

        public ArmorShop(IEnumerable<IArmor> available)
        {
            StockShop(available);
        }

        private void StockShop()
        {
            var armors = GatewayProvider.All<Armor>();
            var masterwork = armors.Select(x => new MasterworkArmor(x));
            var total = armors.Concat<IArmor>(masterwork);
            StockShop(total);
        }
    }
}