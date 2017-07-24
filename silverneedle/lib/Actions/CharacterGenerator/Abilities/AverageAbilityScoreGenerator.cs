//-----------------------------------------------------------------------
// <copyright file="AverageAbilityScoreGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Actions.CharacterGenerator.Abilities
{
    using System.Collections.Generic;
    /// <summary>
    /// Generates ability scores with just your basic average score. Useful for a base line or to
    /// make sure things are initialized properly
    /// </summary>
    public class AverageAbilityScoreGenerator : IAbilityScoreGenerator
    {
        /// <summary>
        /// The average score.
        /// </summary>
        private const int AverageScore = 10;

        public List<int> GetScores()
        {
            var scores = new List<int>();
            for(int i = 0; i < 6; i++)
            {
                scores.Add(AverageScore);
            }
            return scores;
        }
    }
}