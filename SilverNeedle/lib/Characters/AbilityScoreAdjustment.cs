// //-----------------------------------------------------------------------
// // <copyright file="AbilityScoreAdjustment.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    /// <summary>
    /// Ability score adjustment.
    /// </summary>
    public class AbilityScoreAdjustment : BasicStatModifier
    {
        /// <summary>
        /// Gets or sets a value indicating whether this modifier was triggered by racial selection
        /// </summary>
        /// <value><c>true</c> if racial modifier; otherwise, <c>false</c>.</value>
        public bool RacialModifier { get; set; }

        /// <summary>
        /// Gets or sets the name of the ability.
        /// </summary>
        /// <value>The name of the ability.</value>
        public AbilityScoreTypes AbilityName { get; set; }
    }
}