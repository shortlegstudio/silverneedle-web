// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    
    /// <summary>
    /// Purchase initial armor for a character
    /// </summary>
    public class PurchaseShield : ICharacterDesignStep
    {
        /// <summary>
        /// The armors available
        /// </summary>
        private EntityGateway<Armor> armors;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGeneration.PurchaseArmor"/> class.
        /// </summary>
        /// <param name="armorRepo">Armor gateway to load from.</param>
        public PurchaseShield(EntityGateway<Armor> armorRepo)
        {
            this.armors = armorRepo;
        }

        public PurchaseShield()
        {
            this.armors = GatewayProvider.Get<Armor>();
        }

        public void ExecuteStep(CharacterSheet character)
        {
            var shields = this.armors.Where(armor =>
                (armor.ArmorType == ArmorType.Shield || armor.ArmorType == ArmorType.TowerShield) &&
                character.Defense.IsProficient(armor) &&
                character.Inventory.CoinPurse.CanAfford(armor));

            if (shields.HasChoices())
            {
                character.Inventory.Purchase(shields.ChooseOne());
            }            
        }
    }
}