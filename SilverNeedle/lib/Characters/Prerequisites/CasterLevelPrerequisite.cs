// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
        /// <summary>
        /// Caster level prerequisite.
        /// </summary>
        public class CasterLevelPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="CasterLevelPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public CasterLevelPrerequisite(string value)
            {
                this.CasterLevel = value;
            }

            /// <summary>
            /// Gets or sets the caster level.
            /// </summary>
            /// <value>The caster level.</value>
            public string CasterLevel { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }
    
}