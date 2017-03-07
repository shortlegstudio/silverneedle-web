//-----------------------------------------------------------------------
// <copyright file="PurchaseInitialArmor.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    
    /// <summary>
    /// Purchase initial armor for a character
    /// </summary>
    public class PurchaseInitialArmor : ICharacterBuildStep
    {
        /// <summary>
        /// The armors available
        /// </summary>
        private IArmorGateway armors;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.PurchaseInitialArmor"/> class.
        /// </summary>
        /// <param name="armorRepo">Armor gateway to load from.</param>
        public PurchaseInitialArmor(IArmorGateway armorRepo)
        {
            this.armors = armorRepo;
        }

        public PurchaseInitialArmor()
        {
            this.armors = GatewayProvider.Instance().Armors;
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
                inventory.EquipItem(armors.ChooseOne());
            }

            var shield = this.armors.FindByArmorType(ArmorType.Shield)
                .WhereProficient(proficiencies).ToList();
                
            if (shield.Count() > 0)
            {
                inventory.EquipItem(shield.ChooseOne());
            }
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            PurchaseArmorAndShield(character.Inventory, character.Defense.ArmorProficiencies);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}