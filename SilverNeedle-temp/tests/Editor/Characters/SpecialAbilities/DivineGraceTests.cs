// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class DivineGraceTests
    {
        [Fact]
        public void ItHasAGoodName()
        {
            var grace = new DivineGrace();
            Assert.That(grace.Name, Is.EqualTo("Divine Grace"));
        }
        
        [Fact]
        public void AddsCharismaBonusToAllSavingsThrows()
        {
            var character = new CharacterSheet();
            character.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 18); //+4
            var startingFortSave = character.Defense.FortitudeSave.TotalValue;
            var startingReflexSave = character.Defense.ReflexSave.TotalValue;
            var startingWillSave = character.Defense.WillSave.TotalValue;

            character.Add(new DivineGrace());
            Assert.That(character.Defense.FortitudeSave.TotalValue, Is.EqualTo(startingFortSave + 4));
            Assert.That(character.Defense.ReflexSave.TotalValue, Is.EqualTo(startingReflexSave + 4));
            Assert.That(character.Defense.WillSave.TotalValue, Is.EqualTo(startingWillSave + 4));
        }
    }
}