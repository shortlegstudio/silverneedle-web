//-----------------------------------------------------------------------
// <copyright file="BasicStatModifier.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    /// <summary>
    /// Basic stat modifier provides a method for tracking what modifiers to apply to various statistics
    /// An example of a modifier might be a feat that provides a +1 dodge bonus to AC
    /// </summary>
    public class BasicStatModifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.BasicStatModifier"/> class.
        /// </summary>
        public BasicStatModifier()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.BasicStatModifier"/> class.
        /// </summary>
        /// <param name="modifier">Modifier amount for the stat</param>
        /// <param name="reason">Reason for this modifier</param>
        public BasicStatModifier(float modifier, string reason)
        {
            this.Modifier = modifier;
            this.Reason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.BasicStatModifier"/> class.
        /// </summary>
        /// <param name="statisticName">Statistic name to modify</param>
        /// <param name="modifier">Modifier amount</param>
        /// <param name="type">Type of modifier (armor, shield, luck, morale, etc...)</param>
        /// <param name="reason">Reason for modifier</param>
        public BasicStatModifier(string statisticName, float modifier, string type, string reason)
        {
            this.Modifier = modifier;
            this.StatisticName = statisticName;
            this.Type = type;
            this.Reason = reason;
        }

        /// <summary>
        /// Gets or sets the modifier.
        /// </summary>
        /// <value>The modifier amount for the statistic</value>
        public virtual float Modifier { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason for the modifier.</value>
        public virtual string Reason { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type of modifier this is.</value>
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the statistic.
        /// </summary>
        /// <value>The name of the statistic.</value>
        public virtual string StatisticName { get; set; }
    }
}