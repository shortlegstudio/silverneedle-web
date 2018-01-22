// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters.SpecialAbilities;

    /// <summary>
    /// Size stats for a character. Provides stats and modifiers
    /// for different sizes
    /// </summary>
    public class SizeStats : IModifiesStats
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
        public ValueStatModifier PositiveSizeModifier { get; private set; }
        public ValueStatModifier NegativeSizeModifier { get; private set; }

        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this class.</value>
        public IList<IValueStatModifier> Modifiers { get; private set; }


        public IList<SpecialAbility> SpecialAbilities { get; private set; }

        /// <summary>
        /// Gets or sets the stealth adj.
        /// </summary>
        /// <value>The stealth adj.</value>
        private ValueStatModifier StealthAdj { get; set; }

        /// <summary>
        /// Gets or sets the fly adj.
        /// </summary>
        /// <value>The fly adj.</value>
        private ValueStatModifier FlyAdj { get; set; }

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
            this.PositiveSizeModifier.Modifier = (int)size;
            this.NegativeSizeModifier.Modifier = -(int)size;

            this.UpdateStealth();
            this.UpdateFly();
        }

        /// <summary>
        /// Setups the skill modifiers.
        /// </summary>
        private void SetupSkillModifiers()
        {
            this.PositiveSizeModifier = new ValueStatModifier("Attack and Defense", 0, "size");
            this.NegativeSizeModifier = new ValueStatModifier("Attack and Defense", 0, "size");
            this.StealthAdj = new ValueStatModifier("Stealth", 0, "size");
            this.FlyAdj = new ValueStatModifier("Fly", 0, "size");


            this.Modifiers = new List<IValueStatModifier>();
            this.Modifiers.Add(this.StealthAdj);
            this.Modifiers.Add(this.FlyAdj);
            this.Modifiers.Add(this.PositiveSizeModifier);
            this.Modifiers.Add(this.NegativeSizeModifier);
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