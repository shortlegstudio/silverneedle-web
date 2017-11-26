// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class NewArcanaTests
    {
        [Fact]
        public void AddsALearnSpellToken()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.SetLevel(9);
            var newArcana = new NewArcana();
            sorcerer.Add(newArcana);
            Assert.True(sorcerer.Contains<LearnSpellToken>());
        }

        [Fact]
        public void AddsMoreTokensAtLaterLevels()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.SetLevel(9);
            var newArcana = new NewArcana();
            sorcerer.Add(newArcana);
            Assert.Equal(1, sorcerer.GetAll<LearnSpellToken>().Count());
        }
    }
}