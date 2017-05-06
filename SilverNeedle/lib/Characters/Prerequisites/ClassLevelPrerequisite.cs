// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
 /// <summary>
        /// Class level prerequisite.
        /// </summary>
        public class ClassLevelPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="ClassLevelPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public ClassLevelPrerequisite(string value)
            {
                var vals = value.Split(' ');
                this.Class = vals[0];
                this.Minimum = int.Parse(vals[1]);
            }

            /// <summary>
            /// Gets or sets the class.
            /// </summary>
            /// <value>The class.</value>
            public string Class { get; set; }

            /// <summary>
            /// Gets or sets the minimum.
            /// </summary>
            /// <value>The minimum.</value>
            public int Minimum { get; set; }

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