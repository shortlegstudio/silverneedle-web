//-----------------------------------------------------------------------
// <copyright file="AbilityScore.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Dice;

    /// <summary>
    /// An ability score for a character. Examples: Strength, Intelligence, Charisma, ...
    /// </summary>
    public class AbilityScore : BasicStat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.AbilityScore"/> class.
        /// </summary>
        public AbilityScore()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.AbilityScore"/> class.
        /// </summary>
        /// <param name="type">Ability type</param>
        /// <param name="val">Value for the ability</param>
        public AbilityScore(AbilityScoreTypes type, int val)
            : base(val)
        {
            this.Name = type;
        }

        /// <summary>
        /// Gets or sets the name. Synonomous with AbilityScoreTypes
        /// </summary>
        /// <value>The name of the ability</value>
        public AbilityScoreTypes Name { get; set; }

        /// <summary>
        /// Gets the base modifier. The modifier is the ability score modifier applied to other statistics
        /// </summary>
        /// <value>The base modifier.</value>
        public int BaseModifier
        {
            get
            {
                return CalculateModifier(this.BaseValue);
            }
        }

        /// <summary>
        /// Gets the total modifier.
        /// </summary>
        /// <value>The total modifier.</value>
        public int TotalModifier
        {
            get
            {
                return CalculateModifier(this.TotalValue);
            }
        }

        /// <summary>
        /// Gets the type of ability score
        /// </summary>
        /// <returns>The type.</returns>
        /// <param name="name">Name of the AbilityScoreType to parse</param>
        public static AbilityScoreTypes GetType(string name)
        {
            return (AbilityScoreTypes)System.Enum.Parse(typeof(AbilityScoreTypes), name, true);
        }

        /// <summary>
        /// Calculates the modifier. Formula is: (val / 2) -5
        /// </summary>
        /// <returns>The modifier.</returns>
        /// <param name="val">The score to use to calculate the modifier</param>
        public static int CalculateModifier(int val)
        {
            return (val / 2) - 5;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.AbilityScore"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Characters.AbilityScore"/>.</returns>
        public override string ToString()
        {
            return string.Format(
                "[AbilityScore: Name={0}, Adjustments={1}, BaseValue={2}, BaseModifier={3}, TotalValue={4}, TotalModifier={5}, SumAdjustments={6}]", 
                this.Name, 
                this.Modifiers, 
                this.BaseValue, 
                this.BaseModifier, 
                this.TotalValue, 
                this.TotalModifier, 
                this.SumBasicModifiers);
        }
    }
}