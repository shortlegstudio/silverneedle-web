// //-----------------------------------------------------------------------
// // <copyright file="IAbilityScores.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A container of ability scores
    /// </summary>
    public interface IAbilityScores
    {
        /// <summary>
        /// Occurs when an ability is modified.
        /// </summary>
        event EventHandler<AbilityModifiedEventArgs> Modified;

        /// <summary>
        /// Gets all of the abilities.
        /// </summary>
        /// <value>The abilities contained.</value>
        IEnumerable<AbilityScore> Abilities { get; }

        /// <summary>
        /// Gets an ability.
        /// </summary>
        /// <returns>The ability to get.</returns>
        /// <param name="ability">Ability name to find.</param>
        AbilityScore GetAbility(AbilityScoreTypes ability);

        /// <summary>
        /// Gets an ability.
        /// </summary>
        /// <returns>The ability to get.</returns>
        /// <param name="ability">Ability name to find.</param>
        AbilityScore GetAbility(string ability);

        /// <summary>
        /// Gets the score of an ability.
        /// </summary>
        /// <returns>The score of the abiltiy.</returns>
        /// <param name="ability">Ability to find.</param>
        int GetScore(AbilityScoreTypes ability);

        /// <summary>
        /// Gets the score of an ability.
        /// </summary>
        /// <returns>The score of the ability.</returns>
        /// <param name="ability">Ability to find.</param>
        int GetScore(string ability);

        /// <summary>
        /// Gets the modifier for the ability
        /// </summary>
        /// <returns>The modifier amount.</returns>
        /// <param name="ability">Ability to find.</param>
        int GetModifier(AbilityScoreTypes ability);

        /// <summary>
        /// Gets the modifier for the ability.
        /// </summary>
        /// <returns>The modifier amount.</returns>
        /// <param name="ability">Ability to find.</param>
        int GetModifier(string ability);

        /// <summary>
        /// Sets the score for an ability
        /// </summary>
        /// <param name="ability">Ability to set.</param>
        /// <param name="val">Value of the ability.</param>
        void SetScore(AbilityScoreTypes ability, int val);

        /// <summary>
        /// Sets the score for an ability.
        /// </summary>
        /// <param name="ability">Ability to set.</param>
        /// <param name="val">Value of the ability.</param>
        void SetScore(string ability, int val);
    }
}