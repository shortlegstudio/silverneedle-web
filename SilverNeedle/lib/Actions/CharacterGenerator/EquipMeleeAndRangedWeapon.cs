//-----------------------------------------------------------------------
// <copyright file="EquipMeleeAndRangedWeapon.cs" company="Short Leg Studio, LLC">
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
    using SilverNeedle.Utility;

    /// <summary>
    /// Equip melee and ranged weapon selects the weapons for this character to prepare (and purchase)
    /// </summary>
    public class EquipMeleeAndRangedWeapon : ICharacterDesignStep
    {
        /// <summary>
        /// The weapon gateway.
        /// </summary>
        private EntityGateway<Weapon> weaponGateway;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.EquipMeleeAndRangedWeapon"/> class.
        /// </summary>
        /// <param name="weapons">Weapons available for purchase</param>
        public EquipMeleeAndRangedWeapon(EntityGateway<Weapon> weapons)
        {
            this.weaponGateway = weapons;
        }

        public EquipMeleeAndRangedWeapon()
        {
            this.weaponGateway = GatewayProvider.Get<Weapon>();
        }

        /// <summary>
        /// Assigns the weapons.
        /// </summary>
        /// <param name="inv">Inventory to assign to.</param>
        /// <param name="proficiencies">Proficiencies for this character.</param>
        public void AssignWeapons(Inventory inv, IEnumerable<WeaponProficiency> proficiencies)
        {
            var melee = this.weaponGateway.FindByProficient(proficiencies).Where(x => x.Type != WeaponType.Ranged).ToList();
            var ranged = this.weaponGateway.FindByProficient(proficiencies).Where(x => x.Type == WeaponType.Ranged).ToList();
            
            if(melee.HasChoices())
                inv.AddGear(melee.ChooseOne());
            
            if(ranged.HasChoices())
                inv.AddGear(ranged.ChooseOne());
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AssignWeapons(character.Inventory, character.Offense.WeaponProficiencies);
        }
    }
}