// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
         /// <summary>
        /// Race prerequisite requires a specific racial type
        /// </summary>
        public class RacePrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="RacePrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public RacePrerequisite(string value)
            {
                this.Race = value;
            }

            /// <summary>
            /// Gets or sets the race.
            /// </summary>
            /// <value>The race the character must be.</value>
            public string Race { get; set; }

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