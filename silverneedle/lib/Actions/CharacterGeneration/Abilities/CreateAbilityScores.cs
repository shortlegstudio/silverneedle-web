// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.Abilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class CreateAbilityScores : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var roller = strategy.AbilityScoreRoller.Instantiate<IAbilityScoreGenerator>();
            var scores = roller.GetScores();
            scores.Sort();
            IEnumerable<AbilityScoreTypes> weightedAbilities = strategy.FavoredAbilities.UniqueList();
            
            for(int i = 0; i < 6; i++)
            {
                var ability = weightedAbilities.ElementAt(i);
                var score = scores.ElementAt(5 - i);
                character.AbilityScores.SetScore(ability, score);
            }            
        }
    }
}