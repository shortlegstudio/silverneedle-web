//-----------------------------------------------------------------------
// <copyright file="ArmorProficiency.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Equipment;

    /// <summary>
    /// Represents the ability to use a specific piece of armor effectively
    /// </summary>
    public class ArmorProficiency
    {
        /// <summary>
        /// Tracks whether the proficiency represents a whole type of armor
        /// </summary>
        private bool isArmorType;

        /// <summary>
        /// The armor type represented if isArmorType is true
        /// </summary>
        private ArmorType armorType;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.ArmorProficiency"/> class.
        /// </summary>
        /// <param name="proficiency">Armor Proficiency Type.</param>
        public ArmorProficiency(string proficiency)
        {
            this.Name = proficiency;
            this.isArmorType = EnumHelpers.TryParse<ArmorType>(proficiency, true, out this.armorType);
        }

        /// <summary>
        /// Gets the name of the proficient armor piece
        /// </summary>
        /// <value>The name of this proficiency</value>
        public string Name { get; private set; }

        /// <summary>
        /// Determines whether this instance is proficient with the specified armor.
        /// </summary>
        /// <returns><c>true</c> if this instance is proficient with the specified armor; otherwise, <c>false</c>.</returns>
        /// <param name="armor">Armor to check proficiency in.</param>
        public bool IsProficient(Armor armor)
        {
            if (this.isArmorType)
            {
                return armor.ArmorType == this.armorType;
            }

            return string.Compare(armor.Name, this.Name, true) == 0;
        }
    }
}