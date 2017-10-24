// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    /// <summary>
    /// Ability score adjustment.
    /// </summary>
    public class AbilityScoreAdjustment : ValueStatModifier
    {
        /// <summary>
        /// Gets or sets a value indicating whether this modifier was triggered by racial selection
        /// </summary>
        /// <value><c>true</c> if racial modifier; otherwise, <c>false</c>.</value>
        public bool ChooseAny { get; set; }

        /// <summary>
        /// Gets or sets the name of the ability.
        /// </summary>
        /// <value>The name of the ability.</value>
        public AbilityScoreTypes AbilityName { get; set; }
    }
}