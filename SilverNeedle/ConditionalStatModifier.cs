//-----------------------------------------------------------------------
// <copyright file="ConditionalStatModifier.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    /// <summary>
    /// A Conditional stat modifier only modifies a statistic when a condition is met
    /// </summary>
    public class ConditionalStatModifier : BasicStatModifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.ConditionalStatModifier"/> class.
        /// </summary>
        /// <param name="condition">Condition of the modifier.</param>
        /// <param name="stat">Statistic to modify.</param>
        /// <param name="modifier">Modifier amount.</param>
        /// <param name="type">Type of modifier.</param>
        /// <param name="reason">Reason for the modifier.</param>
        public ConditionalStatModifier(string condition, string stat, float modifier, string type, string reason)
            : base(stat, modifier, type, reason)
        {
            this.Condition = condition;
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>The condition name for the modifier.</value>
        public string Condition { get; set; }
    }
}