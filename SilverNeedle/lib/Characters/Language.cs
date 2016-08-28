//-----------------------------------------------------------------------
// <copyright file="Language.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;

    /// <summary>
    /// Languages that a character can learn
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Language"/> class.
        /// </summary>
        /// <param name="name">Name of the language.</param>
        /// <param name="desc">Description of the language.</param>
        public Language(string name, string desc)
        {
            this.Name = name;
            this.Description = desc;
        }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
        /// <value>The language name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description of the language.</value>
        public string Description { get; set; }
    }
}