// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
	using Xunit;
    using SilverNeedle.Characters;

	
	public class InitiativeTests {
		[Fact]
		public void InitiativeIsBasedOnDexterity() {
			var abilities = new AbilityScores ();
			abilities.SetScore (AbilityScoreTypes.Dexterity, 18);
			var init = new Initiative (abilities);
			Assert.Equal (4, init.TotalValue);
		}

        [Fact]
        public void IfDexterityChangesInitiativeIsUpdated() {
            var abilities = new AbilityScores();
            abilities.SetScore(AbilityScoreTypes.Dexterity, 16);
            var init = new Initiative(abilities);
            Assert.Equal(3, init.TotalValue);
            abilities.SetScore(AbilityScoreTypes.Dexterity, 10);
            Assert.Equal(0, init.TotalValue);
        }
	}
}