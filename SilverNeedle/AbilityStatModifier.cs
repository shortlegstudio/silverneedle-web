//-----------------------------------------------------------------------
// <copyright file="AbilityStatModifier.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle
{
    using System;
    using SilverNeedle.Characters;

    /// <summary>
    /// Ability stat modifier will represent the modifiers for a stat that affects
    /// a statistic. For example applying constitution bonus to a fortitude save
    /// </summary>
    public class AbilityStatModifier : BasicStatModifier
    {
        /// <summary>
        /// The ability score to pull the modifier from
        /// </summary>
        private AbilityScore abilityScore;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.AbilityStatModifier"/> class.
        /// </summary>
        /// <param name="ability">Ability score to track.</param>
        public AbilityStatModifier(AbilityScore ability)
        {
            this.abilityScore = ability;
        }

        /// <summary>
        /// Gets the modifier. Set throws an exception in this case because modifying this modifier is not allowed
        /// </summary>
        /// <value>The modifier amount for the statistic</value>
        public override float Modifier
        {
            get
            {
                return this.abilityScore.TotalModifier;
            }

            set
            {
                throw new InvalidOperationException("Cannot set the modifier for an ability modifier");
            }
        }
    }
}