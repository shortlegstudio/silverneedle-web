// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
          /// <summary>
        /// Class feature prerequisite.
        /// </summary>
        public class ClassFeaturePrerequisite : IPrerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="ClassFeaturePrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public ClassFeaturePrerequisite(string value)
            {
                this.ClassFeature = value;
            }

            /// <summary>
            /// Gets or sets the class feature.
            /// </summary>
            /// <value>The class feature.</value>
            public string ClassFeature { get; set; }

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