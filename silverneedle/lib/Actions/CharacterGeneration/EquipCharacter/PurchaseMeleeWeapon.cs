//-----------------------------------------------------------------------
// <copyright file="PurchaseMeleeWeapon.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Shops;

    /// <summary>
    /// Equip melee and ranged weapon selects the weapons for this character to prepare (and purchase)
    /// </summary>
    public class PurchaseMeleeWeapon : ICharacterDesignStep
    {
        private WeaponShop shop;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGeneration.PurchaseMeleeWeapon"/> class.
        /// </summary>
        /// <param name="weapons">Weapons available for purchase</param>
        public PurchaseMeleeWeapon(WeaponShop shop)
        {
            this.shop = shop;
        }

        public PurchaseMeleeWeapon()
        {
            this.shop = new WeaponShop();
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var validWeapons = this.shop.GetInventory<IWeapon>().Where(
                weapon => 
                character.Offense.IsProficient(weapon) &&
                weapon.Type != WeaponType.Ranged &&
                character.Inventory.CoinPurse.CanAfford(weapon)
            );

            if(validWeapons.HasChoices())
            {
                character.Inventory.Purchase(validWeapons.ChooseOne());                
            }
        }
    }
}