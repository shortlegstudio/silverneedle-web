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
        public SizeStats()
            : this(CharacterSize.Medium, 72, 180)
        {
        }

        public SizeStats(CharacterSize size, int height, int weight)
        {
            this.SetupSkillModifiers();
            this.SetSize(size, height, weight);
        }

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

        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this class.</value>
        public IList<IValueStatModifier> Modifiers { get; private set; }

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
        }

        /// <summary>
        /// Setups the skill modifiers.
        /// </summary>
        private void SetupSkillModifiers()
        {
            this.PositiveSizeModifier = new ValueStatModifier("Attack and Defense", 0, "size");
            this.Modifiers = new List<IValueStatModifier>();
            this.Modifiers.Add(this.PositiveSizeModifier);
        }
    }
}