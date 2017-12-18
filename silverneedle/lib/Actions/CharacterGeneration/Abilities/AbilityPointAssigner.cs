// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class AbilityPointAssigner : ICharacterDesignStep
    {
        private void AssignByStrategy(CharacterSheet character, WeightedOptionTable<AbilityScoreTypes> abilities)
        {
            var tokens = character.GetAndRemoveAll<AbilityScoreToken>();
            foreach(var token in tokens)
            {
                var modifier = token.CreateAdjustment(abilities.ChooseRandomly());
                character.Add(modifier);
            }            
        }

        public void ExecuteStep(CharacterSheet character)
        {
            AssignByStrategy(character, character.Strategy.FavoredAbilities);
        }
    }
}