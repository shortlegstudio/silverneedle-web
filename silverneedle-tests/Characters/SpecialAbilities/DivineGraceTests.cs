// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class DivineGraceTests
    {
        [Fact]
        public void ItHasAGoodName()
        {
            var grace = new DivineGrace();
            Assert.Equal(grace.Name, "Divine Grace");
        }
        
        [Fact]
        public void AddsCharismaBonusToAllSavingsThrows()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 18); //+4
            var startingFortSave = character.Defense.FortitudeSave.TotalValue;
            var startingReflexSave = character.Defense.ReflexSave.TotalValue;
            var startingWillSave = character.Defense.WillSave.TotalValue;

            character.Add(new DivineGrace());
            Assert.Equal(character.Defense.FortitudeSave.TotalValue, startingFortSave + 4);
            Assert.Equal(character.Defense.ReflexSave.TotalValue, startingReflexSave + 4);
            Assert.Equal(character.Defense.WillSave.TotalValue, startingWillSave + 4);
        }
    }
}