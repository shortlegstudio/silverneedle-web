// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
          /// <summary>
        /// Base attack bonus prerequisite.
        /// </summary>
        public class BaseAttackBonusPrerequisite : IPrerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="BaseAttackBonusPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public BaseAttackBonusPrerequisite(string value)
            {
                this.AttackBonus = value;
            }

            /// <summary>
            /// Gets or sets the attack bonus.
            /// </summary>
            /// <value>The attack bonus.</value>
            public string AttackBonus { get; set; }

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