// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    /// <summary>
    /// Initiative for the character.
    /// </summary>
    public class Initiative : BasicStat
    {
        /// <summary>
        /// The abilities to use for the character.
        /// </summary>
        private AbilityScores abilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Initiative"/> class.
        /// </summary>
        /// <param name="abilities">Abilities scores for the character.</param>
        public Initiative(AbilityScores abilities)
            : base(StatNames.Initiative)
        {
            this.abilities = abilities;
            this.AddModifier(new AbilityStatModifier(abilities.GetAbility(AbilityScoreTypes.Dexterity)));
        }
    }
}