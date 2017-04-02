//-----------------------------------------------------------------------
// <copyright file="PurchaseMeleeWeapon.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Equip melee and ranged weapon selects the weapons for this character to prepare (and purchase)
    /// </summary>
    public class PurchaseMeleeWeapon : ICharacterDesignStep
    {
        /// <summary>
        /// The weapon gateway.
        /// </summary>
        private EntityGateway<Weapon> weaponGateway;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.PurchaseMeleeWeapon"/> class.
        /// </summary>
        /// <param name="weapons">Weapons available for purchase</param>
        public PurchaseMeleeWeapon(EntityGateway<Weapon> weapons)
        {
            this.weaponGateway = weapons;
        }

        public PurchaseMeleeWeapon()
        {
            this.weaponGateway = GatewayProvider.Get<Weapon>();
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var validWeapons = this.weaponGateway.Where(
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