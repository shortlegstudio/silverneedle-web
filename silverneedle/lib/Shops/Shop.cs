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

    public class Shop : IShop
    {
        protected List<IGear> inventory = new List<IGear>();

        public Shop() { }

        public Shop(IEnumerable<IGear> startingInventory)
        {
            StockShop(startingInventory);
        }

        public IEnumerable<IGear> GetInventory()
        {
            return inventory;
        }

        public IEnumerable<T> GetInventory<T>()
        {
            return inventory.OfType<T>();
        }

        public IEnumerable<IGear> GetAffordableInventory(int maximumValue)
        {
            return inventory.Where(inventoryItem => inventoryItem.Value < maximumValue);
        }

        public void SellItem(IGear item)
        {
            inventory.Remove(item);
        }

        protected void StockShop(IEnumerable<IGear> available)
        {
            inventory.Add(available);
        }
    }
}