//-----------------------------------------------------------------------
// <copyright file="IAbilityScoreGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Mechanics.CharacterGenerator.Abilities
{
    using System;
    using SilverNeedle.Characters;

    /// <summary>
    /// Ability Score Generator
    /// </summary>
    public interface IAbilityScoreGenerator
    {
        /// <summary>
        /// Assigns the abilities.
        /// </summary>
        /// <param name="scores">Scores to assign</param>
        void AssignAbilities(AbilityScores scores);
    }
}