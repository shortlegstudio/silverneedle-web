// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;

    public class AbilityPointAssignerTests
    {
        [Fact]
        public void AssignsAbilityPointsBasedOnStrategy()
        {
            var character = new CharacterSheet();
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 10);
            character.Add(new AbilityScoreToken(2, "Racial Choice"));

            var strategy = new CharacterBuildStrategy();
            strategy.FavoredAbilities.AddEntry(AbilityScoreTypes.Strength, 100000);

            var abilityPointAssigner = new AbilityPointAssigner();
            abilityPointAssigner.ExecuteStep(character, strategy);

            Assert.Equal(12, character.AbilityScores.GetScore(AbilityScoreTypes.Strength));
        }
    }
}