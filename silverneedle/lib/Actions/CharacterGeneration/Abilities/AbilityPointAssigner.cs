
namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class AbilityPointAssigner : ICharacterDesignStep
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

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AssignByStrategy(character, strategy.FavoredAbilities);
        }
    }
}