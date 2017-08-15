// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using SilverNeedle.Characters;
using SilverNeedle.Shops;

namespace SilverNeedle.Actions.CharacterGeneration
{
    public class PurchaseMagicalGear : ICharacterDesignStep
    {
        protected MagicShop magicShop;

        public PurchaseMagicalGear()
        {
            magicShop = new MagicShop();
        }

        public PurchaseMagicalGear(MagicShop injectedShop)
        {
            this.magicShop = injectedShop;
        }
        
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            //TODO: Spending limit should be specified by strategy
            var spend = System.Math.Min(2000000, character.Inventory.CoinPurse.Value);
            var purchased = 0;
            while(spend > 0 && purchased < 3)
            {
                var items = magicShop.GetAffordableInventory(spend);
                if(items.HasChoices())
                {
                    var buy = items.ChooseOne();
                    magicShop.SellItem(buy);
                    character.Inventory.Purchase(buy);
                    spend -= buy.Value;
                    purchased++;
                } else {
                    spend = 0;
                }
            }        
        }
    }
}