//-----------------------------------------------------------------------
// <copyright file="SizeStats.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Size stats for a character. Provides stats and modifiers
    /// for different sizes
    /// </summary>
    public class SizeStats : ISizeStats, IModifiesStats
    {
        /// <summary>
        /// The stealth modifier for different sizes
        /// </summary>
        private const int StealthModifier = 4;

        /// <summary>
        /// The flight modifier for different sizes.
        /// </summary>
        private const int FlyModifier = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.SizeStats"/> class.
        /// </summary>
        public SizeStats()
            : this(CharacterSize.Medium, 72, 180)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.SizeStats"/> class.
        /// </summary>
        /// <param name="size">Size class of the character.</param>
        public SizeStats(CharacterSize size)
            : this(size, 72, 180)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.SizeStats"/> class.
        /// </summary>
        /// <param name="size">Size class of the character.</param>
        /// <param name="height">Height of the character.</param>
        /// <param name="weight">Weight of the character.</param>
        public SizeStats(CharacterSize size, int height, int weight)
        {
            this.SetupSkillModifiers();
            this.SetSize(size, height, weight);
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The character size.</value>
        public CharacterSize Size { get; private set; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <value>The weight.</value>
        public int Weight { get; private set; }

        /// <summary>
        /// Gets the size modifier.
        /// </summary>
        /// <value>The size modifier.</value>
        public int SizeModifier { get; private set; }

        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this class.</value>
        public IList<BasicStatModifier> Modifiers { get; private set; }


        public IList<SpecialAbility> SpecialAbilities { get; private set; }

        /// <summary>
        /// Gets or sets the stealth adj.
        /// </summary>
        /// <value>The stealth adj.</value>
        private BasicStatModifier StealthAdj { get; set; }

        /// <summary>
        /// Gets or sets the fly adj.
        /// </summary>
        /// <value>The fly adj.</value>
        private BasicStatModifier FlyAdj { get; set; }

        /// <summary>
        /// Sets the size of the character.
        /// </summary>
        /// <param name="size">Size category of character.</param>
        /// <param name="height">Height of character.</param>
        /// <param name="weight">Weight of character.</param>
        public void SetSize(CharacterSize size, int height, int weight)
        {
            this.Size = size;
            this.Height = height;
            this.Weight = weight;
            this.SizeModifier = (int)size;

            this.UpdateStealth();
            this.UpdateFly();
        }

        /// <summary>
        /// Setups the skill modifiers.
        /// </summary>
        private void SetupSkillModifiers()
        {
            this.StealthAdj = new BasicStatModifier("Stealth", 0, "size", "Size");
            this.FlyAdj = new BasicStatModifier("Fly", 0, "size", "Size");

            this.Modifiers = new List<BasicStatModifier>();
            this.Modifiers.Add(this.StealthAdj);
            this.Modifiers.Add(this.FlyAdj);
        }

        /// <summary>
        /// Updates the stealth.
        /// </summary>
        private void UpdateStealth()
        {
            // Small = 4, Large = -4....
            this.StealthAdj.Modifier = this.GetSkillMultiplier() * StealthModifier;
        }

        /// <summary>
        /// Updates the fly skill adjustment
        /// </summary>
        private void UpdateFly()
        {
            this.FlyAdj.Modifier = this.GetSkillMultiplier() * FlyModifier;
        }

        /// <summary>
        /// Gets the skill multiplier.
        /// </summary>
        /// <returns>The skill multiplier.</returns>
        private int GetSkillMultiplier()
        {
            // 8 == 4
            // 4 == 3
            // 2 == 2
            // 1 == 1
            int mod = (int)this.Size;
            if (Math.Abs(mod) == 8)
            {
                mod = 4 * Math.Sign(mod);
            }
            else if (Math.Abs(mod) == 4)
            {
                mod = 3 * Math.Sign(mod);
            }

            return mod;
        }
    }
}