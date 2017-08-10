// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Shops
{
    using System.Collections.Generic;
    using SilverNeedle.Equipment;
    public interface IShop
    {
        IEnumerable<IGear> GetInventory();
        IEnumerable<T> GetInventory<T>();
        
        IEnumerable<IGear> GetAffordableInventory(int maximumValue);

        void SellItem(IGear item);
    }
}