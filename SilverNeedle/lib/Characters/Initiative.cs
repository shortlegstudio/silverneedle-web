//-----------------------------------------------------------------------
// <copyright file="Initiative.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using SilverNeedle.Characters;

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
            : base()
        {
            this.abilities = abilities;
            this.AddModifier(new AbilityStatModifier(abilities.GetAbility(AbilityScoreTypes.Dexterity)));
        }
    }
}