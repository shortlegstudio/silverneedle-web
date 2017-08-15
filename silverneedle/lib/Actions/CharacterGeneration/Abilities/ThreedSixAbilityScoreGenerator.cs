// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.Abilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class ThreedSixAbilityScoreGenerator : IAbilityScoreGenerator
    {
        public List<int> GetScores()
        {
            List<int> scores = new List<int>();
            var diceCup = new Cup(Die.GetDice(DiceSides.d6, 3));
            for(int i = 0; i < 6; i++) {
                scores.Add(diceCup.Roll());
            }            
            return scores;
        }
    }
}