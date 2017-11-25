// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using SilverNeedle.Characters.Magic;

namespace SilverNeedle.Characters.Prerequisites
{
        /// <summary>
        /// Caster level prerequisite.
        /// </summary>
        public class CasterLevelPrerequisite : IPrerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="CasterLevelPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public CasterLevelPrerequisite(string casterLevel)
            {
                this.CasterLevel = casterLevel.ToInteger();
            }

            /// <summary>
            /// Gets or sets the caster level.
            /// </summary>
            /// <value>The caster level.</value>
            public int CasterLevel { get; private set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public bool IsQualified(CharacterSheet character)
            {
                var spellcasting = character.Get<ISpellCasting>();
                if(spellcasting == null)
                    return false;

                return spellcasting.CasterLevel >= this.CasterLevel;
            }
        }
    
}