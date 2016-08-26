using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Mechanics.CharacterGenerator;
using SilverNeedle.Mechanics.CharacterGenerator.Abilities;
using System.Linq;
using System.Collections.Generic;

namespace Actions {
	[TestFixture]
	public class LevelUpGeneratorTests {
		CharacterSheet character;

		[SetUp]
		public void SetUp() {
			character = new CharacterSheet (new List<Skill>());
			var abGen = new AverageAbilityScoreGenerator ();
			abGen.AssignAbilities (character.AbilityScores);
			var cls = new Class ();
			character.SetClass (cls);
		}



	    [Test]
	    public void LevelingUpIncrementsTheLevelNumber() {
			var levelUp = new LevelUpGenerator (new HitPointGenerator());
			levelUp.LevelUp (character);
			Assert.AreEqual (2, character.Level);
	    }

		[Test]
		public void HitpointsIncreaseWhenYouLevelUp() {
			var hp = character.MaxHitPoints;
			var levelUp = new LevelUpGenerator (new HitPointGenerator());

			levelUp.LevelUp (character);
			Assert.Greater (character.MaxHitPoints, hp);
		}

		[Test]
		public void EveryFourLevelsYouGetAnExtraAbilityScore() {
			var levelUp = new LevelUpGenerator (new HitPointGenerator());

			levelUp.BringCharacterToLevel (character, 4);

			//At least one ability should be greater than 10 now
			Assert.IsTrue (
				character.AbilityScores.Abilities.Any (x => x.TotalValue > 10)
			);
		}
	}
}