
namespace SilverNeedle.Actions.CharacterGenerator
{
    using System;
    using SilverNeedle.Characters;

    public class AbilityPointAssigner : ICharacterBuildStep
    {
        public void AssignByStrategy(CharacterSheet character, WeightedOptionTable<AbilityScoreTypes> abilities)
        {
            while(character.AbilityScoreTokens.Count > 0)
            {
                var token = character.AbilityScoreTokens.Dequeue();
                var modifier = token.Modifier;
                modifier.AbilityName = abilities.ChooseRandomly();
                character.AbilityScores.AddModifier(modifier);
            }            
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AssignByStrategy(character, strategy.FavoredAbilities);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AssignByStrategy(character, strategy.FavoredAbilities);
        }
    }

}