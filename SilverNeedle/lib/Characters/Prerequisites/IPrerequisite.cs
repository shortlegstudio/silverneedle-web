// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
        /// <summary>
        /// Basic prerequisite abstraction
        /// </summary>
        public interface IPrerequisite
        {
            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns><c>true</c> if this instance is qualified the specified character; otherwise, <c>false</c>.</returns>
            /// <param name="character">Character to assess qualification.</param>
            bool IsQualified(CharacterSheet character);
        }
}