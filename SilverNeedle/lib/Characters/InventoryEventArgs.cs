// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Equipment;

    public class InventoryEventArgs : EventArgs
    {
        public IGear Item { get; set; }
        public Inventory Inventory { get; set; }

        public InventoryEventArgs(IGear item, Inventory inventory)
        {
            this.Item = item;
            this.Inventory = inventory;
        }
    }
}