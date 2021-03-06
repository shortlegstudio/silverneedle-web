// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class PurchaseAdventuringGear : ICharacterDesignStep
    {
        private EntityGateway<Gear> gearGateway;

        public PurchaseAdventuringGear()
        {
            gearGateway = GatewayProvider.Get<Gear>();
        }

        public PurchaseAdventuringGear(EntityGateway<Gear> gear)
        {
            gearGateway = gear;
        }


        public void ExecuteStep(CharacterSheet character)
        {
            //TODO: Max prices should be specified by strategy
            //TODO: Very similiar to other purchasing routines just slightly different...
            var spend = System.Math.Min(20000, character.Inventory.CoinPurse.Value);
            while(spend > 0)
            {
                var items = gearGateway.Where(gear => character.Inventory.CoinPurse.CanAfford(gear));
                if(items.HasChoices())
                {
                    var buy = items.ChooseOne();
                    character.Inventory.Purchase(buy);
                    spend -= buy.Value;
                } else {
                    spend = 0;
                }
            }
        }
    }
}