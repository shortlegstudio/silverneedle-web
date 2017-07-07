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
    public class ArmorShop
    {
        public IEnumerable<IGear> GetInventory()
        {
            return inventory;
        }

        public IEnumerable<T> GetInventory<T>()
        {
            ShortLog.DebugFormat("Searching shop for {0} type.", typeof(T).FullName);
            var found = inventory.OfType<T>();
            ShortLog.DebugFormat("Found {0} items out of {1}.", found.Count(), inventory.Count());
            return found;
        }

        public ArmorShop()
        {
            StockShop();
        }

        public ArmorShop(IEnumerable<IArmor> available)
        {
            StockShop(available);
        }

        private List<IGear> inventory = new List<IGear>();

        private void StockShop()
        {
            var armors = GatewayProvider.All<Armor>();
            var masterwork = armors.Select(x => new MasterworkArmor(x));
            var total = armors.Concat<IArmor>(masterwork);
            StockShop(total);
        }
        private void StockShop(IEnumerable<IArmor> available)
        {
            ShortLog.DebugFormat("Stocking Armor shop with {0} items", available.Count().ToString());
            //Just add all the armors
            inventory.Add(available);
        }
    }
}