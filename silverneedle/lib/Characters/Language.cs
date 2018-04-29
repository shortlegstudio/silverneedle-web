﻿// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    /// <summary>
    /// Languages that a character can learn
    /// </summary>
    public class Language : IGatewayObject, IEnsureUniqueComponent
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

        public Language(IObjectStore data)
        {
            Name = data.GetString("name");
            Description = data.GetString("description");
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

        public bool ReplaceExistingComponent { get { return false; } }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        public override string ToString()
        {
            return string.Format("Language: {0}", this.Name);
        }

        public bool ComponentUniquenessComparison(object obj)
        {
            var language = obj as Language;
            if(language == null)
                return false;

            
            return language.Matches(this.Name); 
        }
    }
}