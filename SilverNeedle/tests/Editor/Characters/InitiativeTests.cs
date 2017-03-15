// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
	using NUnit.Framework;
    using SilverNeedle.Characters;

	[TestFixture]
	public class InitiativeTests {
		[Test]
		public void InitiativeIsBasedOnDexterity() {
			var abilities = new AbilityScores ();
			abilities.SetScore (AbilityScoreTypes.Dexterity, 18);
			var init = new Initiative (abilities);
			Assert.AreEqual (4, init.TotalValue);
		}

        [Test]
        public void IfDexterityChangesInitiativeIsUpdated() {
            var abilities = new AbilityScores();
            abilities.SetScore(AbilityScoreTypes.Dexterity, 16);
            var init = new Initiative(abilities);
            Assert.AreEqual(3, init.TotalValue);
            abilities.SetScore(AbilityScoreTypes.Dexterity, 10);
            Assert.AreEqual(0, init.TotalValue);
        }
	}
}