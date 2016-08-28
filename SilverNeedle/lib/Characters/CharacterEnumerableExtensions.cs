//-----------------------------------------------------------------------
// <copyright file="CharacterEnumerableExtensions.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;

    /// <summary>
    /// Character enumerable extensions. A static class for holding those extension methods that make collections of attributes easier to manage
    /// </summary>
    public static class CharacterEnumerableExtensions
    {
        /// <summary>
        /// Determines if any of these proficiencies is proficient in the specified proficiencies armor.
        /// </summary>
        /// <returns><c>true</c> if is proficient the specified proficiencies armor; otherwise, <c>false</c>.</returns>
        /// <param name="proficiencies">Proficiencies list</param>
        /// <param name="armor">Armor to find proficiency</param>
        public static bool IsProficient(this IEnumerable<ArmorProficiency> proficiencies, Armor armor)
        {
            return proficiencies.Any(x => x.IsProficient(armor));
        }

        /// <summary>
        /// Determines if is proficient the specified proficiencies wpn.
        /// </summary>
        /// <returns><c>true</c> if is proficient the specified proficiencies wpn; otherwise, <c>false</c>.</returns>
        /// <param name="proficiencies">Proficiencies to validate against.</param>
        /// <param name="weapon">Weapon to validate proficiency.</param>
        public static bool IsProficient(this IEnumerable<WeaponProficiency> proficiencies, Weapon weapon)
        {
            return proficiencies.Any(x => x.IsProficient(weapon));
        }

        /// <summary>
        /// Fetches all armors that character is proficient in
        /// </summary>
        /// <returns>The proficient armors.</returns>
        /// <param name="armor">Armor list.</param>
        /// <param name="proficiencies">Proficiency list.</param>
        public static IEnumerable<Armor> WhereProficient(this IEnumerable<Armor> armor, IEnumerable<ArmorProficiency> proficiencies)
        {
            return armor.Where(x => proficiencies.IsProficient(x));
        }
    }
}