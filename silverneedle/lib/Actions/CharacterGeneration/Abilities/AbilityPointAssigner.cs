
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
                character.AbilityScores.AddModifier(modifier);
            }            
        }

        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            AssignByStrategy(character, strategy.FavoredAbilities);
        }
    }
}