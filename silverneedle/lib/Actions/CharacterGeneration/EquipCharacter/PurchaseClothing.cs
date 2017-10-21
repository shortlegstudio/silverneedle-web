// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.EquipCharacter
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    public class PurchaseClothing : ICharacterDesignStep
    {
        private EntityGateway<Clothes> clothing;

        public PurchaseClothing()
        {
            clothing = GatewayProvider.Get<Clothes>();
        }

        public PurchaseClothing(EntityGateway<Clothes> clothingGateway)
        {
            this.clothing = clothingGateway;
        }

        public void ExecuteStep(CharacterSheet character)
        {
            var strategy = character.Strategy;
            var items = clothing.Where(x => 
                character.Inventory.CoinPurse.CanAfford(x) &&
                strategy.GetOptions<string>("clothes").Contains(x.Name)
            );
            if(items.HasChoices())
            {
                character.Inventory.Purchase(items.ChooseOne());
            }
        }
    }
}