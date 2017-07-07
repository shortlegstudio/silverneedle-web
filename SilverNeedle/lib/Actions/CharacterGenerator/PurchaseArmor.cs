// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Shops;
    
    /// <summary>
    /// Purchase initial armor for a character
    /// </summary>
    public class PurchaseArmor : ICharacterDesignStep
    {
        private ArmorShop armorShop;
        public PurchaseArmor()
        {
            armorShop = new ArmorShop();
        }

        public PurchaseArmor(ArmorShop armorShop)
        {
            this.armorShop = armorShop;
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var validArmors = new ArmorType[] {
                ArmorType.None,
                ArmorType.Light,
                ArmorType.Medium,
                ArmorType.Heavy
            };
            var armors = this.armorShop.GetInventory<IArmor>().Where(armor => 
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