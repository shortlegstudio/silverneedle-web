// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
         /// <summary>
        /// Feat prerequisite.
        /// </summary>
        public class FeatPrerequisite : IPrerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="FeatPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public FeatPrerequisite(string value)
            {
                this.Feat = value;
            }

            /// <summary>
            /// Gets or sets the feat.
            /// </summary>
            /// <value>The feat.</value>
            public string Feat { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }
   
}