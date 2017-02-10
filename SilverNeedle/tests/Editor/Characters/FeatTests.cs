using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle.Characters;

namespace Characters {

	[TestFixture]
	public class FeatTests {

		[SetUp]
		public void SetUp() {
		}
			

		[Test]
		public void FeatsKnowWhetherYouQualify() {
			var smartCharacter = new CharacterSheet (new List<Skill>());
			smartCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 15);
			var dumbCharacter = new CharacterSheet (new List<Skill>());
			dumbCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 5);

			var CombatExpertise = new Feat();
			CombatExpertise.Prerequisites.Add(new Prerequisites.AbilityPrerequisite("Intelligence 13"));

			Assert.IsTrue (CombatExpertise.IsQualified (smartCharacter));
			Assert.IsFalse (CombatExpertise.IsQualified (dumbCharacter));
		}		

		[Test]
        public void IfFeatIsAlreadySelectedItCannotBeSelectedAgain()
        {
            var character = new CharacterSheet (new List<Skill>());
            var basicFeat = new Feat();
            character.AddFeat(basicFeat);
            Assert.IsFalse(basicFeat.IsQualified(character));
        }
	}
}
