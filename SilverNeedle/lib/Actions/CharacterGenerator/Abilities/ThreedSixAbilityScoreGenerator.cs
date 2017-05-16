// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.Abilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class ThreedSixAbilityScoreGenerator : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            CreateAndAssignScores(character.AbilityScores, strategy.FavoredAbilities);
        }
        private void CreateAndAssignScores(AbilityScores abilities, WeightedOptionTable<AbilityScoreTypes> abilityPref)
        {
            //Roll up 6 scores
            List<int> scores = new List<int>();
            IEnumerable<AbilityScoreTypes> weightedAbilities = abilityPref.UniqueList();
            var diceCup = new Cup(Die.GetDice(DiceSides.d6, 3));
            
            //Create sorted list of items
            for(int i = 0; i < 6; i++) {
                scores.Add(diceCup.Roll());
            }            
            scores.Sort();

            for(int i = 0; i < 6; i++)
            {
                var ability = weightedAbilities.ElementAt(i);
                var score = scores.ElementAt(5 - i);
                abilities.SetScore(ability, score);
            }            
        }
    }
}