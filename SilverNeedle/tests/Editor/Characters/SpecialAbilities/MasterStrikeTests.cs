// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class MasterStrikeTests
    {
        [Test]
        public void AddsSpecialAttackWithDCToSave()
        {
            var character = new CharacterSheet();
            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14); //+2
            var rogue = new Class("Rogue");
            character.SetClass(rogue);
            character.SetLevel(20);
            character.Add(new MasterStrike());

            var specialAttack = character.Offense.Attacks().First(x => x.Name == "Master Strike");
            Assert.That(specialAttack.SaveDC, Is.EqualTo(22));
        }
    }
}