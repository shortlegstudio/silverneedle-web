//-----------------------------------------------------------------------
// <copyright file="PurchaseRangedWeapon.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
    /// Equip melee and ranged weapon selects the weapons for this character to prepare (and purchase)
    /// </summary>
    public class PurchaseRangedWeapon : ICharacterDesignStep
    {
        private WeaponShop shop;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.PurchaseRangedWeapon"/> class.
        /// </summary>
        /// <param name="weapons">Weapons available for purchase</param>
        public PurchaseRangedWeapon(WeaponShop shop)
        {
            this.shop = shop;
        }

        public PurchaseRangedWeapon()
        {
            this.shop = new WeaponShop();
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var ranged = this.shop.GetInventory<IWeapon>().Where(weapon =>
                character.Offense.IsProficient(weapon) &&
                weapon.Type == WeaponType.Ranged &&
                character.Inventory.CoinPurse.CanAfford(weapon));

            if(ranged.HasChoices())
                character.Inventory.Purchase(ranged.ChooseOne());
        }
    }
}