// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Shops
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;
    
    public class Shop
    {
        protected List<IGear> inventory = new List<IGear>();
        public IEnumerable<IGear> GetInventory()
        {
            return inventory;
        }

        public IEnumerable<T> GetInventory<T>()
        {
            return inventory.OfType<T>();
        }
    }
}