//-----------------------------------------------------------------------
// <copyright file="IArmorGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Equipment.Gateways
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;

    /// <summary>
    /// Armor gateway provides access to armor data
    /// </summary>
    public interface IArmorGateway
    {
        /// <summary>
        /// Returns all armors
        /// </summary>
        /// <returns>All the armors loaded</returns>
        IEnumerable<Armor> All();

        /// <summary>
        /// Gets the armor by name.
        /// </summary>
        /// <returns>The armor</returns>
        /// <param name="name">Name of armor.</param>
        Armor GetByName(string name);

        /// <summary>
        /// Finds the type of the by armor.
        /// </summary>
        /// <returns>The armor by type.</returns>
        /// <param name="type">Type of armor.</param>
        IEnumerable<Armor> FindByArmorType(ArmorType type);

        /// <summary>
        /// Finds the by armor types.
        /// </summary>
        /// <returns>The armors matching types.</returns>
        /// <param name="types">Types of armor.</param>
        IEnumerable<Armor> FindByArmorTypes(params ArmorType[] types);

        /// <summary>
        /// Finds armors by proficiency.
        /// </summary>
        /// <returns>Armors that are valid for proficiency</returns>
        /// <param name="proficiencies">The proficiencies to search by.</param>
        IEnumerable<Armor> FindByProficiency(IEnumerable<ArmorProficiency> proficiencies);
    }
}