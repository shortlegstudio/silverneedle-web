// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Actions.CharacterGeneration.Abilities;
    using System.Collections.Generic;

    
    public class LevelUpCharacterTests {
        //Handles moving characters to Level 2 and beyond
        CharacterSheet character;

        public LevelUpCharacterTests() {
            character = new CharacterSheet(CharacterStrategy.Default());
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 10);
            character.AbilityScores.SetScore(AbilityScoreTypes.Dexterity, 10);
            character.AbilityScores.SetScore(AbilityScoreTypes.Constitution, 10);
            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 10);
            character.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 10);
            character.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 10);
            var cls = new Class ();
            character.SetClass (cls);
        }



        [Fact]
        public void LevelingUpIncrementsTheLevelNumber() {
            var levelUp = new LevelUpCharacter ();
            levelUp.LevelUp (character);
            Assert.Equal (2, character.Level);
        }

        [Fact]
        public void EveryFourLevelsYouGetAnExtraAbilityScore() {
            var levelUp = new LevelUpCharacter ();

            levelUp.BringCharacterToLevel (character, 4);

            //Should have an abilityScore Token
            Assert.True (
                character.GetAll<AbilityScoreToken>().Count() > 0				
            );
        }

        [Fact]
        public void EveryOtherLevelAddsAFeatToken() {
            //Level one feats are currently handled outside this
            var levelUp = new LevelUpCharacter();
            levelUp.BringCharacterToLevel(character, 3);
            Assert.Equal(1, character.FeatTokens.Count());
        }

        [Fact]
        public void IncreasesBaseAttackBonus() {
            var levelUp = new LevelUpCharacter();
            var level = new Level(2);
            character.Class.BaseAttackBonusRate = 1;
            Assert.Equal(0, character.Offense.BaseAttackBonus.TotalValue);
            levelUp.LevelUp(character);
            Assert.Equal(1, character.Offense.BaseAttackBonus.TotalValue);
        }

        [Fact]
        public void IncreasesSavingsThrowsOnLevelUp()
        {
            character.Class.WillSaveRate = 1;
            character.Class.FortitudeSaveRate = 1;
            character.Class.ReflexSaveRate = 1;
            Assert.Equal(0, character.Defense.WillSave.TotalValue);
            Assert.Equal(0, character.Defense.ReflexSave.TotalValue);
            Assert.Equal(0, character.Defense.FortitudeSave.TotalValue);
            
            
            var levelUp = new LevelUpCharacter();
            var level = new Level(2);
            levelUp.LevelUp(character);
            Assert.Equal(1, character.Defense.WillSave.TotalValue);
            Assert.Equal(1, character.Defense.ReflexSave.TotalValue);
            Assert.Equal(1, character.Defense.FortitudeSave.TotalValue);
        }
    }
}