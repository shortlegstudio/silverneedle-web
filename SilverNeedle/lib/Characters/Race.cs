//-----------------------------------------------------------------------
// <copyright file="Race.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle;
    using SilverNeedle.Dice;

    /// <summary>
    /// Represents a race for a character. This is selected once
    /// </summary>
    public class Race
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Race"/> class.
        /// </summary>
        public Race()
        {
            this.AbilityModifiers = new List<AbilityScoreAdjustment>();
            this.Traits = new List<string>();
            this.AvailableLanguages = new List<string>();
            this.KnownLanguages = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name of the race
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the ability modifiers.
        /// </summary>
        /// <value>The ability modifiers.</value>
        public IList<AbilityScoreAdjustment> AbilityModifiers { get; private set; }

        /// <summary>
        /// Gets the traits.
        /// </summary>
        /// <value>The traits.</value>
        public IList<string> Traits { get; private set; }

        /// <summary>
        /// Gets the known languages that a character of this race will always know
        /// </summary>
        /// <value>The known languages.</value>
        public IList<string> KnownLanguages { get; private set; }

        /// <summary>
        /// Gets the available languages that are available for intelligent characters
        /// </summary>
        /// <value>The available languages.</value>
        public IList<string> AvailableLanguages { get; private set; }

        /// <summary>
        /// Gets or sets the size setting.
        /// </summary>
        /// <value>The size setting.</value>
        public CharacterSize SizeSetting { get; set; }

        /// <summary>
        /// Gets or sets the height range dice.
        /// </summary>
        /// <value>The height range.</value>
        public Cup HeightRange { get; set; }

        /// <summary>
        /// Gets or sets the weight range dice.
        /// </summary>
        /// <value>The weight range.</value>
        public Cup WeightRange { get; set; }

        /// <summary>
        /// Gets or sets the base movement speed.
        /// </summary>
        /// <value>The base movement speed.</value>
        public int BaseMovementSpeed { get; set; }

        public Maturity Maturity { get; set; }
    }
}