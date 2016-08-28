//-----------------------------------------------------------------------
// <copyright file="Trait.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;

    /// <summary>
    /// A trait is some basic innate attribute of the character. Usually positive
    /// </summary>
    public class Trait : IModifiesStats, IProvidesSpecialAbilities
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Trait"/> class.
        /// </summary>
        public Trait()
        {
            this.Modifiers = new List<BasicStatModifier>();
            this.SpecialAbilities = new List<SpecialAbility>();
            this.Tags = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this class.</value>
        public IList<BasicStatModifier> Modifiers { get; private set; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public IList<string> Tags { get; private set; }


        public IList<SpecialAbility> SpecialAbilities { get; private set; }
    }
}