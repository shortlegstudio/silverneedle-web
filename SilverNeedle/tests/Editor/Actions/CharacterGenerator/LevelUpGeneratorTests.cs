using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Actions.CharacterGenerator.Abilities;
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
			var levelUp = new LevelUpGenerator (new HitPointRoller());
			levelUp.LevelUp (character);
			Assert.AreEqual (2, character.Level);
	    }

		[Test]
		public void HitpointsIncreaseWhenYouLevelUp() {
			var hp = character.MaxHitPoints;
			var levelUp = new LevelUpGenerator (new HitPointRoller());

			levelUp.LevelUp (character);
			Assert.Greater (character.MaxHitPoints, hp);
		}

		[Test]
		public void EveryFourLevelsYouGetAnExtraAbilityScore() {
			var levelUp = new LevelUpGenerator (new HitPointRoller());

			levelUp.BringCharacterToLevel (character, 4);

			//At least one ability should be greater than 10 now
			Assert.IsTrue (
				character.AbilityScores.Abilities.Any (x => x.TotalValue > 10)
			);
		}

		[Test]
		public void AssignsFeatTokensToCharacters() {
            var levelUp = new LevelUpGenerator(new HitPointRoller());
            var abilities = new LevelAbility[] {
                new LevelAbility("Bonus Feat", "combat", "Feat Token")
            };

            var levelOne = new Level(2, abilities);
            character.Class.Levels.Add(levelOne);

            levelUp.LevelUp(character);
            Assert.AreEqual(1, character.FeatTokens.Count);
		}
	}
}