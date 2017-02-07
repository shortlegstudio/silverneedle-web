//-----------------------------------------------------------------------
// <copyright file="IAbilityScoreGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator.Abilities
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
        void RandomScores(AbilityScores scores);
        void StrategyScores(AbilityScores abilities, WeightedOptionTable<AbilityScoreTypes> preferred);
    }
}