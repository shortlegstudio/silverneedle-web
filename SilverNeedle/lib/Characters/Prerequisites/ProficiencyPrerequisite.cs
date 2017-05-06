// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
            /// <summary>
        /// Proficiency prerequisite.
        /// </summary>
        public class ProficiencyPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="ProficiencyPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public ProficiencyPrerequisite(string value)
            {
                this.Proficiency = value;
            }

            /// <summary>
            /// Gets or sets the proficiency.
            /// </summary>
            /// <value>The proficiency.</value>
            public string Proficiency { get; set; }

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