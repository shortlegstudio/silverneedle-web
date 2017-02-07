
namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle.Characters;

    public class AbilityPointAssigner
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
    }

}