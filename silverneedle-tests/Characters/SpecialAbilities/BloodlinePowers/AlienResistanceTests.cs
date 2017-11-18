// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using Xunit;

    public class AlienResistanceTests
    {
        [Fact]
        public void AddsSpellResistanceEqualToLevelPlusTen()
        {
            var character = CharacterTestTemplates.Sorcerer();
            var alien = new AlienResistance();
            character.Add(alien);
            character.SetLevel(5);
            var defense = character.Get<DefenseStats>();
            Assert.Equal(15, defense.SpellResistance.TotalValue);
        }
    }
}