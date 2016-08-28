//-----------------------------------------------------------------------
// <copyright file="Armor.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Equipment
{
    using System;
    using SilverNeedle.Equipment;

    /// <summary>
    /// Armor is for protecting the character from damage
    /// </summary>
    public class Armor : IEquipment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Armor"/> class.
        /// </summary>
        public Armor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Armor"/> class.
        /// </summary>
        /// <param name="name">Name of armor.</param>
        /// <param name="armorClass">Armor Class.</param>
        /// <param name="weight">Weight in pounds.</param>
        /// <param name="maxDexBonus">Max dexterity bonus.</param>
        /// <param name="armorCheckPenalty">Armor check penalty.</param>
        /// <param name="arcaneSpell">Arcane spell.</param>
        /// <param name="armorType">Armor type.</param>
        public Armor(
            string name,
            int armorClass,
            float weight,
            int maxDexBonus,
            int armorCheckPenalty,
            int arcaneSpell,
            ArmorType armorType)
        {
            this.Name = name;
            this.ArmorClass = armorClass;
            this.Weight = weight;
            this.MaximumDexterityBonus = maxDexBonus;
            this.ArmorCheckPenalty = armorCheckPenalty;
            this.ArcaneSpellFailureChance = arcaneSpell;
            this.ArmorType = armorType;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the armor.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight of the armor.</value>
        public float Weight { get; set; }

        /// <summary>
        /// Gets or sets the armor class.
        /// </summary>
        /// <value>The armor class of the armor.</value>
        public int ArmorClass { get; set; }

        /// <summary>
        /// Gets or sets the maximum dexterity bonus.
        /// </summary>
        /// <value>The maximum dexterity bonus.</value>
        public int MaximumDexterityBonus { get; set; }

        /// <summary>
        /// Gets or sets the armor check penalty.
        /// </summary>
        /// <value>The armor check penalty.</value>
        public int ArmorCheckPenalty { get; set; }

        /// <summary>
        /// Gets or sets the arcane spell failure chance.
        /// </summary>
        /// <value>The arcane spell failure chance.</value>
        public int ArcaneSpellFailureChance { get; set; }

        /// <summary>
        /// Gets or sets the type of the armor.
        /// </summary>
        /// <value>The type of the armor.</value>
        public ArmorType ArmorType { get; set; }
    }
}