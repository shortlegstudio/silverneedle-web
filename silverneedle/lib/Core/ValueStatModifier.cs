// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using SilverNeedle.Serialization;
    /// <summary>
    /// Basic stat modifier provides a method for tracking what modifiers to apply to various statistics
    /// An example of a modifier might be a feat that provides a +1 dodge bonus to AC
    /// </summary>
    public class ValueStatModifier : IValueStatModifier
    {
        public ValueStatModifier()
        {
        }

        public ValueStatModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.ValueStatModifier"/> class.
        /// </summary>
        /// <param name="modifier">Modifier amount for the stat</param>
        public ValueStatModifier(float modifier)
        {
            this.Modifier = modifier;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.ValueStatModifier"/> class.
        /// </summary>
        /// <param name="statisticName">Statistic name to modify</param>
        /// <param name="modifier">Modifier amount</param>
        /// <param name="type">Type of modifier (armor, shield, luck, morale, etc...)</param>
        public ValueStatModifier(string statisticName, float modifier, string type)
        {
            this.Modifier = modifier;
            this.StatisticName = statisticName;
            this.ModifierType = type;
        }

        /// <summary>
        /// Gets or sets the modifier.
        /// </summary>
        /// <value>The modifier amount for the statistic</value>
        [ObjectStore("modifier")]
        public virtual float Modifier { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type of modifier this is.</value>
        [ObjectStoreOptional("modifier-type")]
        public virtual string ModifierType { get; set; }

        /// <summary>
        /// Gets or sets the name of the statistic.
        /// </summary>
        /// <value>The name of the statistic.</value>
        [ObjectStore("name")]
        public virtual string StatisticName { get; set; }
        [ObjectStoreOptional("condition")]
        public string Condition { get; set; }
        [ObjectStoreOptional("stat-type")]
        public string StatisticType { get; set; }
    }
}