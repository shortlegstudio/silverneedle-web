// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    
    /// <summary>
    /// Purchase initial armor for a character
    /// </summary>
    public class PurchaseArmor : ICharacterDesignStep
    {
        /// <summary>
        /// The armors available
        /// </summary>
        private EntityGateway<Armor> armors;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.PurchaseArmor"/> class.
        /// </summary>
        /// <param name="armorRepo">Armor gateway to load from.</param>
        public PurchaseArmor(EntityGateway<Armor> armorRepo)
        {
            this.armors = armorRepo;
        }

        public PurchaseArmor()
        {
            this.armors = GatewayProvider.Get<Armor>();
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var validArmors = new ArmorType[] {
                ArmorType.None,
                ArmorType.Light,
                ArmorType.Medium,
                ArmorType.Heavy
            };
            var armors = this.armors.Where(armor => 
                validArmors.Any(x => x == armor.ArmorType) &&
                character.Defense.IsProficient(armor) &&
                character.Inventory.CoinPurse.CanAfford(armor)
            );

            if (armors.HasChoices())
            {
                var armor = armors.ChooseOne();
                ShortLog.DebugFormat("Purchasing {0}", armor.Name);
                character.Inventory.Purchase(armor);
            }
        }
    }
}