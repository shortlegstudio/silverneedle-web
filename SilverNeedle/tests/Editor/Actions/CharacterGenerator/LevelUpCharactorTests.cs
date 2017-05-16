// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using System.Collections.Generic;

	[TestFixture]
	public class LevelUpCharacterTests {
        //Handles moving characters to Level 2 and beyond
		CharacterSheet character;

		[SetUp]
		public void SetUp() {
			character = new CharacterSheet (new List<Skill>());
			var abGen = new AverageAbilityScoreGenerator ();
			abGen.Process (character, new CharacterBuildStrategy());
			var cls = new Class ();
			character.SetClass (cls);
		}



	    [Test]
	    public void LevelingUpIncrementsTheLevelNumber() {
			var levelUp = new LevelUpCharacter ();
			levelUp.LevelUp (character);
			Assert.AreEqual (2, character.Level);
		}

		[Test]
		public void EveryFourLevelsYouGetAnExtraAbilityScore() {
			var levelUp = new LevelUpCharacter ();

			levelUp.BringCharacterToLevel (character, 4);

			//Should have an abilityScore Token
			Assert.IsTrue (
				character.AbilityScoreTokens.Count > 0				
			);
		}

		[Test]
		public void EveryOtherLevelAddsAFeatToken() {
            //Level one feats are currently handled outside this
            var levelUp = new LevelUpCharacter();
            levelUp.BringCharacterToLevel(character, 3);
            Assert.AreEqual(1, character.FeatTokens.Count);
		}

		[Test]
		public void AssignsFeatTokensToCharacters() {
            var levelUp = new LevelUpCharacter();
            var levelOne = new Level(2);
            levelOne.FeatTokens.Add(new FeatToken());
            character.Class.Levels.Add(levelOne);

            levelUp.LevelUp(character);
            Assert.AreEqual(1, character.FeatTokens.Count);
		}

        [Test]
        public void IncreasesBaseAttackBonus() {
            var levelUp = new LevelUpCharacter();
            var level = new Level(2);
            character.Class.BaseAttackBonusRate = 1;
            Assert.AreEqual(0, character.Offense.BaseAttackBonus.TotalValue);
            levelUp.LevelUp(character);
            Assert.AreEqual(1, character.Offense.BaseAttackBonus.TotalValue);
        }

        [Test]
        public void IncreasesSavingsThrowsOnLevelUp()
        {
            character.Class.WillSaveRate = 1;
            character.Class.FortitudeSaveRate = 1;
            character.Class.ReflexSaveRate = 1;
            Assert.AreEqual(0, character.Defense.WillSave.TotalValue);
            Assert.AreEqual(0, character.Defense.ReflexSave.TotalValue);
            Assert.AreEqual(0, character.Defense.FortitudeSave.TotalValue);
            
            
            var levelUp = new LevelUpCharacter();
            var level = new Level(2);
            levelUp.LevelUp(character);
            Assert.AreEqual(1, character.Defense.WillSave.TotalValue);
            Assert.AreEqual(1, character.Defense.ReflexSave.TotalValue);
            Assert.AreEqual(1, character.Defense.FortitudeSave.TotalValue);
        }
	}
}