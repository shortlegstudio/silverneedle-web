// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.Abilities
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