//-----------------------------------------------------------------------
// <copyright file="StandardAbilityScoreGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Actions.CharacterGeneration.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Dice;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;
    
    /// <summary>
    /// Generates ability scores by rolling 4d6 for each and selecting the top 3 dice
    /// </summary>
    public class StandardAbilityScoreGenerator : IAbilityScoreGenerator
    {
        public List<int> GetScores()
        {
            List<int> scores = new List<int>();
            
            //Create sorted list of items
            for(int i = 0; i < 6; i++) {
                scores.Add(Roll4d6());
            }            
            return scores;
        }

        /// <summary>
        /// Roll4d6 To get ability Score
        /// </summary>
        /// <returns>Top 3 dice summed</returns>
        private int Roll4d6()
        {
            var diceCup = new Cup(Die.GetDice(DiceSides.d6, 4));
            diceCup.Roll();
            return diceCup.SumTop(3);
        }
    }
}