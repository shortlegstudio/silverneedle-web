// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    
    
    public class AbilityScoresTests {
        private CharacterSheet character;
        private AbilityScores Subject;

        public AbilityScoresTests() {
            character = CharacterTestTemplates.AverageBob();
            Subject = character.AbilityScores;
        }

        [Fact]
        public void AbilityScoresCanSetAbilities() {
            Subject.SetScore (AbilityScoreTypes.Charisma, 15);
            Assert.Equal (15, Subject.GetScore ("Charisma"));
        }

        [Fact]
        public void YouMayGetTheAbilityModifier() {
            Subject.SetScore (AbilityScoreTypes.Charisma, 12);

            Assert.Equal (1, Subject.GetModifier (AbilityScoreTypes.Charisma));
        }
    }
}