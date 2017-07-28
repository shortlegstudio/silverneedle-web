// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    
    
    public class AbilityScoresTests {
        private AbilityScores Subject;

        public AbilityScoresTests() {
            Subject = new AbilityScores ();
        }

        [Fact]
        public void AbilityScoresContainerHasAllTheStatsSetToZero() {
            Assert.Equal(0, Subject.GetScore(AbilityScoreTypes.Strength));
            Assert.Equal(0, Subject.GetScore(AbilityScoreTypes.Wisdom));
            Assert.Equal(0, Subject.GetScore("Intelligence"));
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

        [Fact]
        public void YouCanCopyOneSetOfAbilityScoresToAnother() {
            var abilityScores = new AbilityScores ();
            var copyFrom = new AbilityScores ();
            foreach (var e in copyFrom.Abilities) {
                e.SetValue (15);
            }

            abilityScores.Copy (copyFrom);

            foreach (var e in abilityScores.Abilities) {
                Assert.Equal (e.TotalValue, copyFrom.GetScore (e.Ability));
            }
        }
    }
}