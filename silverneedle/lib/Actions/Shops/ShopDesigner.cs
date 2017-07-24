// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Shops
{
    using System;
    using SilverNeedle.Serialization;
    using SilverNeedle.Shops;

    public class ShopDesigner : IGatewayObject, IShopDesignStep
    {
        public string Name { get; private set; }
        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public void Process(IShop shop)
        {
            throw new NotImplementedException();
        }
    }
}