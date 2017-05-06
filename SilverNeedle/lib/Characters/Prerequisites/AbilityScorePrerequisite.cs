// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    /// <summary>
    /// Ability prerequisite are based on an ability score
    /// </summary>
    public class AbilityPrerequisite : Prerequisite
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AbilityPrerequisite"/> class.
        /// </summary>
        /// <param name="value">Requisite string.</param>
        public AbilityPrerequisite(AbilityScoreTypes ability, int value)
        {
            // TODO: Determine whether it's logical to be parsing at this level
            this.Ability = ability;
            this.Minimum = value;
        }

        /// <summary>
        /// Gets or sets the ability for the prerequisite.
        /// </summary>
        /// <value>The ability type.</value>
        public AbilityScoreTypes Ability { get; set; }

        /// <summary>
        /// Gets or sets the minimum value for the ability
        /// </summary>
        /// <value>The minimum value.</value>
        public int Minimum { get; set; }

        /// <summary>
        /// Determines whether this instance is qualified the specified character.
        /// Character must have equal to or more of the specified ability
        /// </summary>
        /// <returns>true if the character is qualified</returns>
        /// <param name="character">Character to assess qualification.</param>
        public override bool IsQualified(CharacterSheet character)
        {
            return character.AbilityScores.GetScore(this.Ability) >= this.Minimum;
        }
    }
}