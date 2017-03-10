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
    using SilverNeedle.Utility;
    
    /// <summary>
    /// Purchase initial armor for a character
    /// </summary>
    public class PurchaseInitialArmor : ICreateStep
    {
        /// <summary>
        /// The armors available
        /// </summary>
        private EntityGateway<Armor> armors;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.PurchaseInitialArmor"/> class.
        /// </summary>
        /// <param name="armorRepo">Armor gateway to load from.</param>
        public PurchaseInitialArmor(EntityGateway<Armor> armorRepo)
        {
            this.armors = armorRepo;
        }

        public PurchaseInitialArmor()
        {
            this.armors = GatewayProvider.Get<Armor>();
        }

        /// <summary>
        /// Purchases the armor and shield.
        /// </summary>
        /// <param name="inventory">Inventory to assign to</param>
        /// <param name="proficiencies">The armor proficiencies of the character</param> 
        public void PurchaseArmorAndShield(Inventory inventory, IEnumerable<ArmorProficiency> proficiencies)
        {
            var armors = this.armors.FindByArmorTypes(
                             ArmorType.None,
                             ArmorType.Light,
                             ArmorType.Medium,
                             ArmorType.Heavy)
                .WhereProficient(proficiencies).ToList();

            if (armors.Count() > 0)
            {
                var armor = armors.ChooseOne();
                ShortLog.DebugFormat("Purchasing {0}", armor.Name);
                inventory.EquipItem(armors.ChooseOne());
            }
            
            var shields = this.armors.FindByArmorType(ArmorType.Shield)
                .WhereProficient(proficiencies).ToList();
                
            if (shields.Count() > 0)
            {
                var shield = shields.ChooseOne();
                ShortLog.DebugFormat("Purchasing {0}", shield.Name);
                inventory.EquipItem(shield);
            }
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            ShortLog.DebugFormat("Purchasing Armor");
            PurchaseArmorAndShield(character.Inventory, character.Defense.ArmorProficiencies);
        }
    }
}